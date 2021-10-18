using System;
using Microsoft.Extensions.DependencyInjection;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Services.DiscriminatedUnions.Clients;
using MyBlazorApp.Services.WeatherForecasts.Clients;
using MyBlazorApp.Utility;

namespace MyBlazorApp.BlazorClient.Maui
{
    public static class StartupExtensions
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
            //services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            services.AddWeatherForecastsHttpClient(new Uri("127.0.0.1:5001//"))
                    .AddDiscriminatedUnionsHttpClient(new Uri("127.0.0.1:5001//"));
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