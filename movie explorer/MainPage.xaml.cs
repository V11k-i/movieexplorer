using movie_explorer.Resources.Splash;

namespace movie_explorer
{
    public partial class MainPage : ContentPage
    {
        MovieService _movieService = new();
        public MainPage()
        {
            InitializeComponent();
            
            _movieService.DataAvailable += OnDataAvailable;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _movieService.GetData();
        }
        private async void OnDataAvailable(object? sender, string e)
        {
            
            await Shell.Current.DisplayAlert("Success", e, "OK");
        }

      

    }
}
