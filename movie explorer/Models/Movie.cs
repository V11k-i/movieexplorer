using System.Collections.Specialized;
using System.ComponentModel;

namespace movie_explorer.Models
{
    public class Movie : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler? PropertyChanged;
        public string title { get; set; }
        public int year { get; set; }
        public List<string> genre { get; set; }
        public string formatGenre => string.Join(", ", genre);
        public string id => MovieId.toHash(title, year);
        public string director { get; set; }
        public double rating { get; set; }
        public string emoji { get; set; }
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
