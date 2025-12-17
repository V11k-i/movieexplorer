using movie_explorer.Models;
using movie_explorer.ViewModels;

namespace movie_explorer
{
    public partial class MainPage : ContentPage
    {
        private  MainPageViewModel _MainPageViewModel;
        public MainPage()
        {
            _MainPageViewModel = new MainPageViewModel();
            BindingContext = _MainPageViewModel;
            InitializeComponent();
           
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _MainPageViewModel.LoadMovies.Execute(this);
            
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
