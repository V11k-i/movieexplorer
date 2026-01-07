namespace movie_explorer
{
    public partial class AppShell : Shell
    {
        public AppShell(IServiceProvider sp)
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MovieDetailsPage), typeof(MovieDetailsPage));

            Favs.ContentTemplate = new DataTemplate(() => sp.GetRequiredService<Favourites>());
            Home.ContentTemplate = new DataTemplate(() => sp.GetRequiredService<MainPage>());
            Settings.ContentTemplate = new DataTemplate(() => sp.GetRequiredService<Settings>());
        }
    }
}
