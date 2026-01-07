using movie_explorer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace movie_explorer.ViewModels
{
    
    public class FavouritesViewModel : ViewModelBase
    {
        private readonly MovieUser _movieUser;
        private readonly MovieService _movieService;

        public ObservableCollection<Movie> favs => _movieUser.FavouriteMovies;

        private Movie? _selectedMovie;
        public Movie? SelectedMovie
        {
            get => _selectedMovie;
            set => SetProperty(ref _selectedMovie, value);
        }

        public Command<Movie> ToggleFavouriteCommand { get; }

        public FavouritesViewModel(MovieStore mvStore, MovieUser mvUser, MovieService mvService)
        {
            _movieUser = mvUser;
            _movieService = mvService;

            ToggleFavouriteCommand = new Command<Movie>(movie =>
            {
                if (movie is null) return;

                movie.favourite = !movie.favourite;
                Task.Run(async () => await _movieService.SaveData(mvStore.Movies));
            });
        }
    }
}
