using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace MyBlazorApp.BlazorClient.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .RegisterBlazorMauiWebView()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddBlazorWebView().ConfigureDI();
#if WINDOWS
            builder.Services.AddSingleton<ITrayService, WinUI.TrayService>();
            builder.Services.AddSingleton<INotificationService, WinUI.NotificationService>();
#elif MACCATALYST
            builder.Services.AddSingleton<ITrayService, MacCatalyst.TrayService>();
            builder.Services.AddSingleton<INotificationService, MacCatalyst.NotificationService>();
#endif


            return builder.Build();
        }
    }
}