namespace movie_explorer
{
    public partial class App : Application
    {
        private readonly IServiceProvider _sp;
        public App(IServiceProvider sp)
        {
            InitializeComponent();
            _sp = sp;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        
           => new Window(_sp.GetRequiredService<AppShell>());
        
    }
}