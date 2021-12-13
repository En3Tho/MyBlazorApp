using System;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Services.DiscriminatedUnions.Clients;
using MyBlazorApp.Services.WeatherForecasts.Clients;
using MyBlazorApp.Utility;

namespace MyBlazorApp.BlazorClient.WebAssembly
{
    public static class Startup
    {
        public static WebAssemblyHostBuilder ConfigureOptions(this WebAssemblyHostBuilder builder)
        {
            Console.WriteLine(builder.Configuration.Build().GetDebugView());
            return builder;
        }

        public static WebAssemblyHostBuilder ConfigureLogging(this WebAssemblyHostBuilder builder)
        {
            builder.Logging.Configure(options =>
                options.ActivityTrackingOptions = ActivityTrackingOptions.ParentId
                                                | ActivityTrackingOptions.SpanId
                                                | ActivityTrackingOptions.TraceId);

            return builder;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSingleton(Json.CreateDefaultOptions())
                .AddSingleton<ComponentDataStorage>()
                .AddSingleton(new ThemeSwitch(Theme.Red))

                .AddWeatherForecastsHttpClient(configuration)
                .AddDiscriminatedUnionsHttpClient(configuration);// load theme from user config or smth?

            return services;
        }
    }
}