using System.Collections.Specialized;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Storage;

namespace movie_explorer.Models
{
    
    public class MovieUser
    {
        private const string UserNameKey = "user_name";
        private readonly MovieStore _movieStore;

        public ObservableCollection<Movie> FavouriteMovies { get; } = new();

        public string name { get; private set; } = string.Empty;

        public MovieUser(MovieStore movieStore)
        {
            _movieStore = movieStore;

            name = Preferences.Default.Get(UserNameKey, string.Empty);

          
            _movieStore.Movies.CollectionChanged += Movies_CollectionChanged;

           
            foreach (var movie in _movieStore.Movies)
                AttachMovie(movie);
        }

        public void SetName(string value)
        {
            name = value ?? string.Empty;
            Preferences.Default.Set(UserNameKey, name);
        }

        private void Movies_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Movie movie in e.NewItems)
                    AttachMovie(movie);
            }

            if (e.OldItems != null)
            {
                foreach (Movie movie in e.OldItems)
                    DetachMovie(movie);
            }
        }

        private void AttachMovie(Movie movie)
        {
            movie.PropertyChanged += Movie_PropertyChanged;

            if (movie.favourite)
                AddFavourite(movie);
        }

        private void DetachMovie(Movie movie)
        {
            movie.PropertyChanged -= Movie_PropertyChanged;
            RemoveFavourite(movie);
        }

        private void Movie_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is not Movie movie)
                return;

            if (e.PropertyName == nameof(Movie.favourite))
            {
                if (movie.favourite)
                    AddFavourite(movie);
                else
                    RemoveFavourite(movie);
            }
        }

        private void AddFavourite(Movie movie)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (!FavouriteMovies.Contains(movie))
                    FavouriteMovies.Add(movie);
            });
        }

        private void RemoveFavourite(Movie movie)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (FavouriteMovies.Contains(movie))
                    FavouriteMovies.Remove(movie);
            });
        }
    }
}
