using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Services.DiscriminatedUnions.Clients;
using MyBlazorApp.Services.WeatherForecasts.Clients;
using MyBlazorApp.Utility;
using Photino.Blazor;

namespace MyBlazorApp.BlazorClient.WebWindow
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            Console.WriteLine("Configuring services");
            services.AddServices(configuration);
        }

        public void Configure(DesktopApplicationBuilder app)
        {
            Console.WriteLine("Adding app component");
            app.AddComponent<App>("app");
        }
    }

    public static class StartupExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSingleton(Json.CreateDefaultOptions())
                .AddSingleton<StateStorage>()
                .AddSingleton(new ThemeSwitch(Theme.Red)) // load theme from user config or smth?

                .AddWeatherForecastsHttpClient(configuration)
                .AddDiscriminatedUnionsHttpClient(configuration);

            return services;
        }
    }
}