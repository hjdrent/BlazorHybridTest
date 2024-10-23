using BlazorHybrid.Core.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
#if __ANDROID__
using BlazorHybrid.Services;
#endif

namespace BlazorHybrid
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

#if __ANDROID__
                // Register the ActivityProvider service
                builder.Services.AddSingleton<IActivityProvider, ActivityProvider>();
#endif

            builder.Services.AddSingleton<IPublicClientApplication>(sp =>
            {
                var clientAppBuilder = PublicClientApplicationBuilder.Create("00000000-0000-0000-0000-000000000000")
                    .WithAuthority(AzureCloudInstance.AzurePublic, "11111111-1111-1111-1111-111111111111");
                // Configure the redirect URI based on platform
#if WINDOWS
                    // Use loopback redirect URI for Windows
                    clientAppBuilder = clientAppBuilder.WithRedirectUri("http://localhost:5050");
#endif
#if __ANDROID__
                    // Retrieve the current activity from IActivityProvider
                    var activityProvider = sp.GetRequiredService<IActivityProvider>();
                    var currentActivity = activityProvider.GetCurrentActivity();

                    // Use custom URI scheme for Android
                    clientAppBuilder = clientAppBuilder
                        .WithRedirectUri($"msal{clientid}://auth")
                        .WithParentActivityOrWindow(() => currentActivity);
#endif
                return clientAppBuilder.Build();
            });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<IDataService, DataService>();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
