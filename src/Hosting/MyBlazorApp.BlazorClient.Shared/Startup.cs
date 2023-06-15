using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Services.DiscriminatedUnions.Clients;
using MyBlazorApp.Services.WeatherForecasts.Clients;
using MyBlazorApp.Utility;

namespace MyBlazorApp.BlazorClient.Shared;

class ConsoleLoggerProvider : ILoggerProvider
{
    public void Dispose()
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new ConsoleLogger(categoryName);
    }

    record ConsoleLogger(string CategoryName) : ILogger
    {
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            Console.WriteLine($"{CategoryName} {logLevel} {eventId} {formatter(state, exception)}");
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }
    }
}

public static class HostBuilderExtensions
{
    public static IServiceCollection AddAppClient(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton(Json.CreateDefaultOptions())
            .AddSingleton<StateStorage>()
            .AddSingleton(new ThemeSwitch(Theme.Red))

            .AddWeatherForecastsHttpClient(configuration)
            .AddDiscriminatedUnionsHttpClient(configuration)
            .AddLogging();

        return services;
    }

    public static ILoggingBuilder ConfigureLogging(this ILoggingBuilder builder, IConfiguration configuration)
    {
        builder.AddConfiguration();
        builder.AddProvider(new ConsoleLoggerProvider());
        builder.AddConfiguration(configuration.GetSection("Logging"));
        builder.Configure(options =>
            options.ActivityTrackingOptions = ActivityTrackingOptions.ParentId
                                              | ActivityTrackingOptions.SpanId
                                              | ActivityTrackingOptions.TraceId);

        return builder;
    }

    public static IConfigurationBuilder ConfigureOptions(this IConfigurationBuilder builder)
    {
        return builder;
    }
}