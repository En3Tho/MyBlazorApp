using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Services.DiscriminatedUnions.DependencyInjection;
using MyBlazorApp.Services.WeatherForecasts.Hosting.DependencyInjection;
using MyBlazorApp.Utility;

namespace MyBlazorApp.BlazorClient.BlazorServer
{
    public static class StartupExtensions
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSingleton(Json.CreateDefaultOptions())
                .AddSingleton<ComponentDataFactory>()
                .AddSingleton(new ThemeSwitch(Theme.Red))

                .AddWeatherForecastsService()
                .AddDiscriminatedUnionsService();

            return services;
        }
    }
}