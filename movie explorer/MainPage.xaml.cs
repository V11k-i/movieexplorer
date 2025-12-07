using movie_explorer.Models;

namespace movie_explorer
{
    public partial class MainPage : ContentPage
    {
       
        public MainPage()
        {
            InitializeComponent();
            
           
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
