using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Hosting;
using MyBlazorApp.Api.HttpClients;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Utility;
using System;
using System.Net.Http;

namespace MyBlazorApp.BlazorClient.Maui
{
    public class Startup : IStartup
    {
        public void Configure(IAppHostBuilder appBuilder)
        {
            appBuilder
                .UseFormsCompatibility()
                .RegisterBlazorMauiWebView(typeof(Startup).Assembly)
                .UseMicrosoftExtensionsServiceProviderFactory()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                })
                .ConfigureServices(services =>
                {
                    services
                        .AddBlazorWebView()
                        .ConfigureDI();
                });
        }
    }

    public static class StartupEx
    {
        private static IServiceCollection ConfigureSingletons(this IServiceCollection services)
        {
            services.AddSingleton(Json.CreateDefaultOptions());
            services.AddSingleton<ComponentDataProvider>();
            services.AddSingleton(new ThemeSwitch(Theme.Red)); // load theme from user config or smth?
                                                               // global StateHasChanged event to simplify programming?
            return services;
        }

        private static IServiceCollection ConfigureScoped(this IServiceCollection services)
        {
            services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("localhost:5001/") });
            services.AddScoped<WeatherForecastApiV1HttpClient>();
            services.AddScoped<DiscriminatedUnionApiV1HttpClient>();
            return services;
        }

        private static IServiceCollection ConfigureTransient(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection ConfigureDI(this IServiceCollection services) =>
            services.ConfigureScoped()
                    .ConfigureSingletons()
                    .ConfigureTransient();
    }
}
