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
      
        private MovieStore _movieStore;
        private Movie _selectedmovie;
        public ObservableCollection<Movie> Movies => _movieStore.Movies;
        public Movie SelectedMovie
        {

            get => _selectedmovie;
            set => SetProperty(ref _selectedmovie, value);
        }
        public ICommand LoadMovies { get; }
        public MainPageViewModel(MovieStore mvstore)
        {
           _movieStore = mvstore;
        }
        public ICommand FavouriteCmd => new Command<Movie>(movie =>
        {
            
            movie.favourite = !movie.favourite;

        });

      //  public async Task LoadMoviesAsync() => wdada


    }
}
