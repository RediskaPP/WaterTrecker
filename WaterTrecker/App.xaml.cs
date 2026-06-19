using Microsoft.Extensions.DependencyInjection;

namespace WaterTrecker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            bool darkMode = Preferences.Get("dark_mode", false);

            UserAppTheme = darkMode ? AppTheme.Dark : AppTheme.Light;

            MainPage = new AppShell();
        }

        //protected override Window CreateWindow(IActivationState? activationState)
        //{
        //    return new Window(new AppShell());
        //}
    }
}