using Microsoft.Extensions.DependencyInjection;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Utility;
using MyBlazorApp.Services.DiscriminatedUnions.DependencyInjection;
using MyBlazorApp.Services.WeatherForecasts.Clients.DependencyInjection;

namespace MyBlazorApp.BlazorClient.Maui
{
    public static class StartupExtensions
    {
        private static IServiceCollection ConfigureSingletons(this IServiceCollection services) =>
            services
                .AddSingleton(Json.CreateDefaultOptions())
                .AddSingleton<ComponentDataProvider>()
                .AddSingleton(new ThemeSwitch(Theme.Red)); // load theme from user config or smth?// global StateHasChanged event to simplify programming?

        private static IServiceCollection ConfigureScoped(this IServiceCollection services) =>
                services.AddWeatherForecastsHttpClient()
                        .AddDiscriminatedUnionsHttpClient();
            

        private static IServiceCollection ConfigureTransient(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection ConfigureDI(this IServiceCollection services) =>
            services
                .ConfigureScoped()
                .ConfigureSingletons()
                .ConfigureTransient();
    }
}