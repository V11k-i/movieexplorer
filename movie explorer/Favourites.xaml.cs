using System;
using Microsoft.Maui.ApplicationModel;
using movie_explorer.Models;
using movie_explorer.ViewModels;

namespace movie_explorer;

public partial class Favourites : ContentPage
{
	public Favourites(FavouritesViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection == null || e.CurrentSelection.Count == 0)
            return;

        if (e.CurrentSelection[0] is Movie movie)
        {
            if (BindingContext is FavouritesViewModel vm)
                vm.SelectedMovie = null;

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(MovieDetailsPage)}?id={Uri.EscapeDataString(movie.id)}");
            });
        }
    }
}