using movie_explorer.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace movie_explorer.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly MovieUser _user;

        public ObservableCollection<string> ThemeOptions { get; } = new() { "System", "Light", "Dark" };

        private string _userName = string.Empty;
        public string UserName
        {
            get => _userName;
            set
            {
                if (SetProperty(ref _userName, value))
                    _user.SetName(_userName);
            }
        }

        private string _selectedTheme = "System";
        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (SetProperty(ref _selectedTheme, value))
                    ApplyTheme(_selectedTheme);
            }
        }

        public ICommand ApplyThemeCommand { get; }

        public SettingsViewModel(MovieUser user)
        {
            _user = user;
            _userName = _user.name;

            _selectedTheme = AppSettings.GetTheme();
            ApplyThemeCommand = new Command(() => ApplyTheme(SelectedTheme));
        }

        private void ApplyTheme(string theme)
        {
            AppSettings.SetTheme(theme);

            if (Application.Current is null)
                return;

            Application.Current.UserAppTheme = theme switch
            {
                "Light" => AppTheme.Light,
                "Dark" => AppTheme.Dark,
                _ => AppTheme.Unspecified
            };
        }
    }
}
