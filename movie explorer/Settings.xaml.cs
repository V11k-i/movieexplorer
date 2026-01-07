using movie_explorer.ViewModels;

namespace movie_explorer;

public partial class Settings : ContentPage
{
	public Settings(SettingsViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}