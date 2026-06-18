using Microsoft.Extensions.Logging;
using WaterTrecker.Services;
using WaterTrecker.Pages;

namespace WaterTrecker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
            builder.Services.AddSingleton<WaterService>();

            builder.Services.AddTransient<DashboardPage>();
            builder.Services.AddTransient<AddRecordPage>();
            builder.Services.AddTransient<HistoryPage>();
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<ProfilePage>();
#endif

            return builder.Build();
        }
    }
}
