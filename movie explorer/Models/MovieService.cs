using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.IO;
using Microsoft.Maui.Storage;

namespace movie_explorer.Models
{
    
     public class MovieService // a service that is used to download and operate  JSON file containing information avbout movies
    {
        public event EventHandler<string>? DataAvailable;
        private readonly HttpClient _httpClient = new();
        public async Task<List<Movie> >GetData() // downloads json file from the source and returns it as a string 
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, "movies.json");
            string json;
            if (!(File.Exists(path)))
            {
                json = await _httpClient.GetStringAsync("https://raw.githubusercontent.com/DonH-ITS/jsonfiles/refs/heads/main/moviesemoji.json");
                await File.WriteAllTextAsync(path, json);
                DataAvailable?.Invoke(this, "DEBUG Download Complete");
                
            }
            else
            {
                 json = await File.ReadAllTextAsync(path);
              
            }
            return JsonSerializer.Deserialize<List<Movie>>(json) ?? new List<Movie>();
           
            
        }


    }
}
