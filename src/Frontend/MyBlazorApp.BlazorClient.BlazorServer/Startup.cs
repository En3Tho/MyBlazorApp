using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Services.DiscriminatedUnions.DependencyInjection;
using MyBlazorApp.Services.WeatherForecasts.Hosting.DependencyInjection;
using MyBlazorApp.Utility;

namespace MyBlazorApp.BlazorClient.BlazorServer
{
    public static class Startup
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSingleton(Json.CreateDefaultOptions());

            services
                .AddScoped<ComponentDataStorage>()
                .AddScoped(_ => new ThemeSwitch(Theme.Red))

                .AddWeatherForecastsService()
                .AddDiscriminatedUnionsService();

            return services;
        }
    }
}