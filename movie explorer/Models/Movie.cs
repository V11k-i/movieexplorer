using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Microsoft.Maui.Controls;

namespace movie_explorer.Models
{
    public class Movie : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler? PropertyChanged;
        public string title { get; set; }
        public int year { get; set; }
        public List<string> genre { get; set; }
        [JsonIgnore]
        public string formatGenre => string.Join(", ", genre);

        [JsonIgnore]
        public string id => MovieId.toHash(title, year);
        public string director { get; set; }
        public double rating { get; set; }
        public string emoji { get; set; }

        private string? _posterUrl;
        public string? posterUrl
        {
            get => _posterUrl;
            set
            {
                if (_posterUrl != value)
                {
                    _posterUrl = value;
                    OnPropertyChanged(nameof(posterUrl));
                    OnPropertyChanged(nameof(posterSource));
                }
            }
        }

       
        [JsonIgnore]
        public ImageSource? posterSource
            => string.IsNullOrWhiteSpace(posterUrl)
                ? null
                : new UriImageSource
                {
                    Uri = new Uri(posterUrl),
                    CachingEnabled = true,
                    CacheValidity = TimeSpan.FromDays(14)
                };
        private bool _favourite;
        public bool favourite 
        {
            get => _favourite;
            set
            {   
                if (_favourite != value)
                {
                    _favourite = value;
                    OnPropertyChanged(nameof(favourite));
                }
            }
        }
        protected virtual void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
