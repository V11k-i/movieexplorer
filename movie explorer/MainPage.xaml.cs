using System;
using Microsoft.Maui.ApplicationModel;
using movie_explorer.Models;
using movie_explorer.ViewModels;

namespace movie_explorer
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage( MainPageViewModel viewmodel)
        {
           
            BindingContext = viewmodel;
            InitializeComponent();
           
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //_MainPageViewModel.LoadMovies.Execute(this);
            
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection == null || e.CurrentSelection.Count == 0)
                return;

            if (e.CurrentSelection[0] is Movie movie)
            {
                if (BindingContext is MainPageViewModel vm)
                    vm.SelectedMovie = null;

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Shell.Current.GoToAsync($"{nameof(MovieDetailsPage)}?id={Uri.EscapeDataString(movie.id)}");
                });
            }
        }
    }
}
