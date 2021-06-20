using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlazorApp.Api.HttpClients;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.ComponentsAndPages;
using MyBlazorApp.Utility;

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
            //services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            services
                .AddScoped(_ => new HttpClient { BaseAddress = new Uri("https://localhost:5001/") })
                .AddScoped<WeatherForecastApiV1HttpClient>()
                .AddScoped<DiscriminatedUnionApiV1HttpClient>();

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