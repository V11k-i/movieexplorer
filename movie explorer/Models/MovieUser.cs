using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace movie_explorer.Models
{
    public class MovieUser
    {
        public string name { get; set; }
        public ObservableCollection<Movie> favourites { get; } = new ObservableCollection<Movie>();

    }
}
