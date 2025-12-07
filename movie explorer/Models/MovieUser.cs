using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace movie_explorer.Models
{
    internal class MovieUser
    {
        public string name { get; set; }
        public List<Movie> favourites { get; set; }
        
    }
}
