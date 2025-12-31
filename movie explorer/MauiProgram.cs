using Microsoft.Extensions.Logging;

namespace movie_explorer
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            //services registration
            builder.Services.AddSingleton<Models.MovieStore>();
            builder.Services.AddTransient<Models.MovieService>();

            //viewmodels registration
            builder.Services.AddTransient<ViewModels.MainPageViewModel>();
            builder.Services.AddTransient<ViewModels.FavouritesViewModel>();

            //views registration
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<Favourites>();
            builder.Services.AddTransient<Settings>();
            

#if DEBUG
            builder.Logging.AddDebug();
#endif
           
            return builder.Build();
        }
    }
}
