using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;

namespace movie_explorer.Models
{
    public class MovieStore
    {
        private MovieService _movieService;   
        private ObservableCollection<Movie> _movies = new();
        public ObservableCollection<Movie> Movies
        {
            get => _movies;

        }

        public MovieStore(MovieService movieService)
        {
           _movieService = movieService;
           Task.Run(LoadMoviesAsync);
        }

        private async Task LoadMoviesAsync()
        {
            var list = await _movieService.GetData();
            foreach (var mov in list)
            {
                await MainThread.InvokeOnMainThreadAsync(() => Movies.Add(mov));
            }

           
            Task.Run(async () =>
            {
                await _movieService.PopulatePosterUrlsAsync(Movies);
                await _movieService.SaveData(Movies);
            });
        }
       
    }
}
