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

        }
    }
}
