using System;
using Microsoft.Extensions.DependencyInjection;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Services.DiscriminatedUnions.DependencyInjection;
using MyBlazorApp.Services.WeatherForecasts.Clients.DependencyInjection;
using MyBlazorApp.Utility;
using Photino.Blazor;

namespace MyBlazorApp.BlazorClient.WebWindow
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("Configuring services");
            services.ConfigureDI();
        }

        public void Configure(DesktopApplicationBuilder app)
        {
            Console.WriteLine("Adding app component");
            app.AddComponent<App>("app");
        }
    }

    public static class StartupExtensions
    {
        public static DesktopApplicationBuilder ConfigureOptions(this DesktopApplicationBuilder builder)
        {
            //Console.WriteLine(builder.Configuration.Build().GetDebugView());
            return builder;
        }

        public static DesktopApplicationBuilder ConfigureLogging(this DesktopApplicationBuilder builder)
        {
            // builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
            // builder.Logging.Configure(options =>
            //     options.ActivityTrackingOptions = ActivityTrackingOptions.ParentId
            //                                     | ActivityTrackingOptions.SpanId
            //                                     | ActivityTrackingOptions.TraceId);

            return builder;
        }

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
            services.AddWeatherForecastsHttpClient()
                    .AddDiscriminatedUnionsHttpClient();
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