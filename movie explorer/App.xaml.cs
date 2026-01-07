namespace movie_explorer
{
    public partial class App : Application
    {
        private readonly IServiceProvider _sp;
        public App(IServiceProvider sp)
        {
            InitializeComponent();
            _sp = sp;

	            var theme = Models.AppSettings.GetTheme();
	            UserAppTheme = theme switch
	            {
	                "Light" => AppTheme.Light,
	                "Dark" => AppTheme.Dark,
	                _ => AppTheme.Unspecified
	            };
        }

        protected override Window CreateWindow(IActivationState? activationState)
        
           => new Window(_sp.GetRequiredService<AppShell>());
        
    }
}