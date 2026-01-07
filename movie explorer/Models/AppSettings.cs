using Microsoft.Maui.Storage;

namespace movie_explorer.Models
{
    public static class AppSettings
    {
        private const string ThemeKey = "app_theme"; // System, Light, Dark

        public static string GetTheme() => Preferences.Default.Get(ThemeKey, "System");

        public static void SetTheme(string theme)
            => Preferences.Default.Set(ThemeKey, theme ?? "System");
    }
}
