using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Microsoft.Extensions.Hosting;

public record OpenTelemetryOptions(
    bool ExportTraces = true,
    bool ExportMetrics = true,
    bool ExportLogging = true,
    ExportProcessorType ExportProcessorType = ExportProcessorType.Batch,
    Action<IOpenTelemetryBuilder>? ConfigureBuilder = null,
    Action<OtlpExporterOptions>? ConfigureExporter = null,
    IEnumerable<KeyValuePair<string, object>>? Attributes = null)
{
    public bool ExportEnabled => ExportTraces | ExportMetrics | ExportLogging;
}

public static class Extensions
{
    public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
    {
        builder.Services.AddServiceDiscovery();

        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            // Turn on resilience by default
            http.AddStandardResilienceHandler();

            // Turn on service discovery by default
            http.AddServiceDiscovery();
        });

        return builder;
    }

    public static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder, OpenTelemetryOptions options)
    {
        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
        });

        var otelBuilder = builder.Services.AddOpenTelemetry();

        otelBuilder.ConfigureResource(builder =>
        {
            if (options.Attributes is {} attributes)
                builder.AddAttributes(attributes);
        });

        otelBuilder.WithTracing(tracing =>
        {
            if (builder.Environment.IsDevelopment())
            {
                tracing.SetSampler(new ParentBasedSampler(rootSampler: new AlwaysOnSampler()));
            }

            tracing
                .AddGrpcClientInstrumentation()
                .AddHttpClientInstrumentation(instrumentationOptions =>
                {
                    instrumentationOptions.EnrichWithHttpRequestMessage = (activity, message) =>
                    {
                        if (message.RequestUri is {} uri)
                            activity.DisplayName = uri.IsAbsoluteUri ? uri.GetLeftPart(UriPartial.Path) : uri.ToString();
                    };
                });
        });

        otelBuilder.WithMetrics(metrics =>
        {
            metrics.AddRuntimeInstrumentation()
                .AddBuiltInMeters();
        });

        builder.AddOpenTelemetryExporters(options);

        options.ConfigureBuilder?.Invoke(otelBuilder);

        return builder;
    }

    public static IHostApplicationBuilder AddOpenTelemetryExporters(this IHostApplicationBuilder builder, OpenTelemetryOptions options)
    {
        // https://github.com/open-telemetry/opentelemetry-specification/blob/main/specification/protocol/exporter.md#specifying-headers-via-environment-variables
        if (options.ExportEnabled && string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]))
            throw new InvalidOperationException("OTEL_EXPORTER_OTLP_ENDPOINT must be set to use OpenTelemetry");

        if (options.ExportLogging)
        {
            builder.Services.Configure<OpenTelemetryLoggerOptions>(logging =>
                logging.AddOtlpExporter(exporterOptions =>
                {
                    exporterOptions.ExportProcessorType = options.ExportProcessorType;
                    options.ConfigureExporter?.Invoke(exporterOptions);
                }));
        }

        if (options.ExportMetrics)
        {
            builder.Services.ConfigureOpenTelemetryMeterProvider(metrics =>
                metrics.AddOtlpExporter(exporterOptions =>
                {
                    exporterOptions.ExportProcessorType = options.ExportProcessorType;
                    options.ConfigureExporter?.Invoke(exporterOptions);
                }));
        }

        if (options.ExportTraces)
        {
            builder.Services.ConfigureOpenTelemetryTracerProvider(tracing =>
                tracing.AddOtlpExporter(exporterOptions =>
                {
                    exporterOptions.ExportProcessorType = options.ExportProcessorType;
                    options.ConfigureExporter?.Invoke(exporterOptions);
                }));
        }

        return builder;
    }

    public static MeterProviderBuilder AddBuiltInMeters(this MeterProviderBuilder meterProviderBuilder) =>
        meterProviderBuilder.AddMeter(
            "Microsoft.AspNetCore.Hosting",
            "Microsoft.AspNetCore.Server.Kestrel",
            "System.Net.Http");
}