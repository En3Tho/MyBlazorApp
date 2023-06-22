using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace MyBlazorApp.Server.Shared;

public record OpenTelemetrySettings(bool IsEnabled, string ServiceName, string Endpoint);

public static class HostBuilderExtensions
{
    public static IServiceCollection AddServerTelemetry(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(OpenTelemetrySettings)).Get<OpenTelemetrySettings>()!;
        if (settings.IsEnabled)
        {
            services.AddOpenTelemetry()
                .WithTracing(builder =>
                {
                    builder
                        .SetResourceBuilder(ResourceBuilder
                            .CreateDefault()
                            .AddService(settings.ServiceName)
                            .AddTelemetrySdk())
                        .AddHttpClientInstrumentation()
                        .AddAspNetCoreInstrumentation()
                        .AddOtlpExporter(options =>
                        {
                            options.Endpoint = new(settings.Endpoint);
                        });
                });
        }

        return services;
    }

    public static ILoggingBuilder ConfigureLogging(this ILoggingBuilder builder, IConfiguration configuration)
    {
        builder.Services.AddLogging();
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