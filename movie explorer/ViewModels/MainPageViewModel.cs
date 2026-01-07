using movie_explorer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.Specialized;

namespace movie_explorer.ViewModels
{

    public class MainPageViewModel : ViewModelBase
    {
        private MovieService _movieService;
        private MovieStore _movieStore;
        private Movie? _selectedmovie;
        public ObservableCollection<Movie> Movies => _movieStore.Movies;

       
        public ObservableCollection<Movie> FilteredMovies { get; } = new();

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                    RefreshFilter();
            }
        }
        public Movie? SelectedMovie
        {

            get => _selectedmovie;
            set => SetProperty(ref _selectedmovie, value);
        }
        public ICommand LoadMovies { get; }
        public MainPageViewModel(MovieStore mvStore, MovieService mvService)
        {
           _movieStore = mvStore;
           _movieService = mvService;

           _movieStore.Movies.CollectionChanged += Movies_CollectionChanged;
           RefreshFilter();
        }
        public ICommand FavouriteCmd => new Command<Movie>(movie =>
        {
            movie.favourite = !movie.favourite;
            Task.Run(async() => await _movieService.SaveData(Movies));
        });

        private void Movies_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshFilter();
        }

        private void RefreshFilter()
        {
            var query = (SearchText ?? string.Empty).Trim();
            var q = query.ToLowerInvariant();

            IEnumerable<Movie> matches = Movies;
            if (!string.IsNullOrWhiteSpace(query))
            {
                matches = Movies.Where(m =>
                    (m.title?.ToLowerInvariant().Contains(q) ?? false) ||
                    (m.director?.ToLowerInvariant().Contains(q) ?? false) ||
                    (m.formatGenre?.ToLowerInvariant().Contains(q) ?? false));
            }

            FilteredMovies.Clear();
            foreach (var m in matches)
                FilteredMovies.Add(m);
        }

      //  public async Task LoadMoviesAsync() => wdada


    }
}
