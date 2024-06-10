using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui.Maps;

namespace TripApp
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
                })
                .UseMauiCommunityToolkitMaps("K6KzCHNPP37HrehD2xR2~orgjsbuBtTz29rU4GI4OAQ~AgkYTY0ifcsWcQBI6TOxzh6lWY7VZoDYZo7uk89U7nfv9vAijJscy8GY_ER5ouRp");
                //.UseMauiMaps();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
