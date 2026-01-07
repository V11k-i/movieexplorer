using System;
using Microsoft.Extensions.DependencyInjection;
using movie_explorer.ViewModels;

namespace movie_explorer;

public partial class MovieDetailsPage : ContentPage, IQueryAttributable
{
    private readonly MovieDetailsViewModel _vm;

    // Shell routing may create pages via Activator (no DI). This keeps routing working
    // while still using the DI-registered ViewModel.
    public MovieDetailsPage() : this(ResolveVm())
    {
    }

    public MovieDetailsPage(MovieDetailsViewModel viewModel)
    {
        _vm = viewModel;
        BindingContext = _vm;
        InitializeComponent();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var idObj) && idObj is string id)
        {
            _vm.LoadMovie(id);
        }
    }

    private static MovieDetailsViewModel ResolveVm()
    {
        var services = Application.Current?.Handler?.MauiContext?.Services;
        if (services == null)
            throw new InvalidOperationException("Service provider not available.");

        return services.GetRequiredService<MovieDetailsViewModel>();
    }
}
