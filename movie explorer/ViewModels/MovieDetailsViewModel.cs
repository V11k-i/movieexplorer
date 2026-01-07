using movie_explorer.Models;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace movie_explorer.ViewModels
{
    public class MovieDetailsViewModel : ViewModelBase
    {
        private readonly MovieStore _movieStore;
        private readonly MovieService _movieService;

        private Movie? _movie;
        public Movie? Movie
        {
            get => _movie;
            private set
            {
                if (_movie != null)
                    _movie.PropertyChanged -= Movie_PropertyChanged;

                if (SetProperty(ref _movie, value))
                {
                    if (_movie != null)
                        _movie.PropertyChanged += Movie_PropertyChanged;

                    OnPropertyChanged(nameof(FavouriteButtonText));
                    OnPropertyChanged(nameof(CanToggleFavourite));
                }
            }
        }

        public bool CanToggleFavourite => Movie != null;

        public string FavouriteButtonText
            => Movie == null
                ? ""
                : (Movie.favourite ? "Remove from favourites" : "Add to favourites");

        public Command ToggleFavouriteCommand { get; }

        public MovieDetailsViewModel(MovieStore movieStore, MovieService movieService)
        {
            _movieStore = movieStore;
            _movieService = movieService;

            ToggleFavouriteCommand = new Command(() =>
            {
                if (Movie is null) return;

                Movie.favourite = !Movie.favourite;
                OnPropertyChanged(nameof(FavouriteButtonText));
                Task.Run(async () => await _movieService.SaveData(_movieStore.Movies));
            });
        }

        public void LoadMovie(string id)
        {
            Movie = _movieStore.Movies.FirstOrDefault(m => m.id == id);
        }

        private void Movie_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Models.Movie.favourite))
                OnPropertyChanged(nameof(FavouriteButtonText));
        }
    }
}
