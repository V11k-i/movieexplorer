using movie_explorer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace movie_explorer.ViewModels
{

    public class MainPageViewModel : ViewModelBase
    {
        private MovieService _movieService;

        private Movie _selectedmovie;
        private ObservableCollection<Movie> _movies = new();
        public ObservableCollection<Movie> Movies
        {
            get => _movies;

        }
        public Movie SelectedMovie
        {

            get => _selectedmovie;
            set => SetProperty(ref _selectedmovie, value);
        }
        public ICommand LoadMovies { get; }
        public MainPageViewModel()
        {
            _movieService = new MovieService();
            LoadMovies = new Command(async () => await LoadMoviesAsync()); // command that will be called every time user opens mainpage
        }
        public ICommand FavouriteCmd => new Command<Movie>(movie =>
        {
            //if (movie.favourite == false)
            //{
            //    movie.favourite = true;
            //    movie.emoji += " ⭐";

            //}
            //else
            //{
            //    movie.favourite = false;
            //}
            movie.favourite = !movie.favourite;

        });
        private async Task LoadMoviesAsync()
        {
            Movies.Clear();
            var list = await _movieService.GetData();
            foreach (var mov in list)
                Movies.Add(mov);
        }
        
    }
}
