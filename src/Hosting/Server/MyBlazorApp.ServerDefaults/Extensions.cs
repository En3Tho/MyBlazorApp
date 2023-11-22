using System.Text.Json;
using En3Tho.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyBlazorApp.Utility;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Microsoft.Extensions.Hosting;

public static class Extensions
{
    public static IHostApplicationBuilder AddServerDefaults(this IHostApplicationBuilder builder)
    {
        builder.AddDefaultHealthChecks();

        builder.Services.ConfigureHttpJsonOptions(options =>
            Json.AddFSharpConverters(options.SerializerOptions));

        builder.Services.AddOrReplaceSingleton<JsonSerializerOptions>(serviceProvider =>
            serviceProvider.GetRequiredService<IOptions<JsonOptions>>().Value.SerializerOptions);

        return builder.AddServiceDefaults();
    }

    public static IHostApplicationBuilder AddDefaultHealthChecks(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            // Add a default liveness check to ensure app is responsive
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

        return builder;
    }

    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
        // Uncomment the following line to enable the Prometheus endpoint (requires the OpenTelemetry.Exporter.Prometheus.AspNetCore package)
        // app.MapPrometheusScrapingEndpoint();

        // All health checks must pass for app to be considered ready to accept traffic after starting
        app.MapHealthChecks("/health");

        // Only health checks tagged with the "live" tag must pass for app to be considered alive
        app.MapHealthChecks("/alive", new HealthCheckOptions
        {
            Predicate = r => r.Tags.Contains("live")
        });

        return app;
    }

    public static IHostApplicationBuilder ConfigureServerOpenTelemetry(this IHostApplicationBuilder builder, OpenTelemetryOptions options)
    {
        if (options.Enabled)
        {
            if (options.Logging)
            {
                builder.Logging.AddOpenTelemetry(logging =>
                {
                    logging.IncludeFormattedMessage = true;
                    logging.IncludeScopes = true;
                });
            }

            var otelBuilder = builder.Services.AddOpenTelemetry();

            otelBuilder.ConfigureResource(builder =>
            {
                builder.AddService(options.ServiceName);
                if (options.Attributes is {} attributes)
                    builder.AddAttributes(attributes);
            });

            if (options.Traces)
            {
                otelBuilder.WithTracing(tracing =>
                {
                    if (builder.Environment.IsDevelopment())
                    {
                        tracing.SetSampler(new ParentBasedSampler(rootSampler: new AlwaysOnSampler()));
                    }

                    tracing
                        .AddAspNetCoreInstrumentation(options =>
                        {
                            options.Filter = context =>
                            {
                                if (context.Request.Path.Value is {} path)
                                {
                                    // This is for YARP proxy
                                    return !path.Equals("/opentelemetry.proto.collector.trace.v1.TraceService/Export");
                                }

                                return true;
                            };
                        })
                        .AddGrpcClientInstrumentation()
                        .AddHttpClientInstrumentation();
                });
            }

            if (options.Metrics)
            {
                otelBuilder.WithMetrics(metrics =>
                {
                    metrics.AddRuntimeInstrumentation()
                        .AddBuiltInMeters();
                });
            }

            builder.AddOpenTelemetryExporters(options);
        }

        return builder;
    }
}