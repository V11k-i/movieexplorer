using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.IO;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using Microsoft.Maui.ApplicationModel;
using System.Net.Http.Headers;

namespace movie_explorer.Models
{
    public class MovieService // a service that is used to download and operate JSON file containing information about movies
    {
        string path = Path.Combine(FileSystem.AppDataDirectory, "movies.json");
        public event EventHandler<string>? DataAvailable;
        private readonly HttpClient _httpClient = new();
        private readonly SemaphoreSlim _fileLock = new(1, 1);

        public MovieService()
        {
            // Some public APIs may reject requests without a User-Agent header.
            if (!_httpClient.DefaultRequestHeaders.UserAgent.Any())
                _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("movie_explorer", "1.0"));

            _httpClient.Timeout = TimeSpan.FromSeconds(20);
        }

        public async Task<List<Movie>> GetData() // downloads json file from the source and returns it as a list
        {
            string json;
            if (!(File.Exists(path)))
            {
                json = await _httpClient.GetStringAsync("https://raw.githubusercontent.com/DonH-ITS/jsonfiles/refs/heads/main/moviesemoji.json");
                await File.WriteAllTextAsync(path, json);
                // DataAvailable?.Invoke(this, "DEBUG Download Complete");
            }
            else
            {
                json = await File.ReadAllTextAsync(path);
            }

            return JsonSerializer.Deserialize<List<Movie>>(json) ?? new List<Movie>();
        }

        /// <summary>
        /// Best-effort poster lookup using Cinemeta (Stremio's public metadata service).
        /// No API key required.
        /// Runs safely in the background and fills Movie.posterUrl when missing.
        /// </summary>
        public async Task PopulatePosterUrlsAsync(IEnumerable<Movie> movies)
        {
            foreach (var movie in movies)
            {
                if (!string.IsNullOrWhiteSpace(movie.posterUrl))
                    continue;

                var poster = await TryGetPosterUrlFromCinemetaAsync(movie.title, movie.year);
                if (string.IsNullOrWhiteSpace(poster))
                    continue;

                await MainThread.InvokeOnMainThreadAsync(() => movie.posterUrl = poster);
            }
        }

        private async Task<string?> TryGetPosterUrlFromCinemetaAsync(string title, int year)
        {
            try
            {
                // Cinemeta search endpoint example:
                // https://v3-cinemeta.strem.io/catalog/movie/top/search=interstellar.json
                var term = Uri.EscapeDataString(title);
                var url = $"https://v3-cinemeta.strem.io/catalog/movie/top/search={term}.json";

                var json = await _httpClient.GetStringAsync(url);
                using var doc = JsonDocument.Parse(json);

                if (!doc.RootElement.TryGetProperty("metas", out var metas) || metas.ValueKind != JsonValueKind.Array)
                    return null;

                bool haveBest = false;
                JsonElement best = default;

                foreach (var meta in metas.EnumerateArray())
                {
                    if (!haveBest)
                    {
                        best = meta;
                        haveBest = true;
                    }

                    // Prefer an exact year match when available.
                    if (meta.TryGetProperty("year", out var y) && TryReadInt(y, out var metaYear) && metaYear == year)
                    {
                        best = meta;
                        break;
                    }
                }

                if (!haveBest)
                    return null;

                if (best.TryGetProperty("poster", out var posterProp))
                {
                    var poster = posterProp.GetString();
                    if (!string.IsNullOrWhiteSpace(poster))
                        return ForceHttps(poster);
                }

                // Fallback: build poster URL from imdb id using Metahub.
                if (best.TryGetProperty("id", out var idProp))
                {
                    var id = idProp.GetString();
                    if (!string.IsNullOrWhiteSpace(id))
                        return $"https://images.metahub.space/poster/medium/{id}.jpg";
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private static bool TryReadInt(JsonElement el, out int value)
        {
            value = 0;

            try
            {
                if (el.ValueKind == JsonValueKind.Number)
                    return el.TryGetInt32(out value);

                if (el.ValueKind == JsonValueKind.String)
                    return int.TryParse(el.GetString(), out value);
            }
            catch
            {
                // ignore
            }

            return false;
        }

        private static string ForceHttps(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return url;

            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                return "https://" + url.Substring("http://".Length);

            return url;
        }

        public async Task SaveData(ObservableCollection<Movie> movies) // saves the json file to the local storage
        {
            await _fileLock.WaitAsync();
            try
            {
                string json = JsonSerializer.Serialize(movies);
                await File.WriteAllTextAsync(path, json);
            }
            finally
            {
                _fileLock.Release();
            }
        }
    }
}
