using movie_explorer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace movie_explorer.ViewModels
{
    
    internal class MainPageViewModel : ViewModelBase
    {

        private Movie _selectedmovie;
        public ObservableCollection<Movie> Movies {  get; set; }
        public Movie SelectedMovie
        {
            get => _selectedmovie;
            set => SetProperty(ref _selectedmovie, value);
        }
        
    }
}
