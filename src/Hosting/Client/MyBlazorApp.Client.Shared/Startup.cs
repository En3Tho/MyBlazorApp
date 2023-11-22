using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Services.DiscriminatedUnions.Client.V1;
using MyBlazorApp.Services.WeatherForecasts.Client.V1;
using MyBlazorApp.Utility;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace MyBlazorApp.Client.Shared;

class ConsoleWriteLineLoggerProvider : ILoggerProvider
{
    public void Dispose()
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new ConsoleWriteLineLogger(categoryName);
    }

    record ConsoleWriteLineLogger(string CategoryName) : ILogger
    {
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            Console.WriteLine($"{DateTime.Now} {logLevel} {CategoryName} {eventId} {formatter(state, exception)}");
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

public record OpenTelemetrySettings(bool IsEnabled, string ServiceName, string Endpoint, string ExportType = "Batch");

public static class HostBuilderExtensions
{
    public static IServiceCollection AddMyBlazorAppClient(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton(Json.CreateDefaultOptions())
            .AddSingleton<StateStorage>()
            .AddSingleton(new ThemeSwitch(Theme.Red))

            .AddWeatherForecastsHttpClient()
            .AddDiscriminatedUnionsHttpClient()
            .AddClientTelemetry(configuration);

        return services;
    }

    public static IServiceCollection AddClientTelemetry(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(OpenTelemetrySettings)).Get<OpenTelemetrySettings>()!;
        if (settings.IsEnabled)
        {
            services.AddOpenTelemetry()
                .WithTracing(builder =>
                {
                    builder
                        .SetResourceBuilder(
                            ResourceBuilder
                                .CreateDefault()
                                .AddService(settings.ServiceName))
                        .AddHttpClientInstrumentation()
                        .AddOtlpExporter(options =>
                        {
                            options.ExportProcessorType = Enum.Parse<ExportProcessorType>(settings.ExportType);
                            options.Endpoint = new(settings.Endpoint);
                        });
                });
        }

        return services;
    }

    public static ILoggingBuilder ConfigureLogging(this ILoggingBuilder builder, IConfiguration configuration)
    {
        builder.Services.AddLogging();
        builder
            .ClearProviders()
            .AddConfiguration(configuration.GetSection("Logging"))
            .AddProvider(new ConsoleWriteLineLoggerProvider())
            .Configure(options =>
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