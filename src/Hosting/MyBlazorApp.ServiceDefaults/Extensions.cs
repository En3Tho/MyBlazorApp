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
    bool Traces = true,
    bool Metrics = true,
    bool Logging = true,
    ExportProcessorType ExportProcessorType = ExportProcessorType.Batch,
    OtlpExportProtocol Protocol = OtlpExportProtocol.Grpc,
    Action<OpenTelemetryBuilder>? ConfigureBuilder = null,
    IEnumerable<KeyValuePair<string, object>>? Attributes = null)
{
    public bool Enabled => Traces || Metrics || Logging;
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
            http.UseServiceDiscovery();
        });

        return builder;
    }

    public static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder, OpenTelemetryOptions options)
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

            options.ConfigureBuilder?.Invoke(otelBuilder);
        }

        return builder;
    }

    public static IHostApplicationBuilder AddOpenTelemetryExporters(this IHostApplicationBuilder builder, OpenTelemetryOptions options)
    {
        var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

        if (useOtlpExporter)
        {
            builder.Services.Configure<OpenTelemetryLoggerOptions>(logging =>
                logging.AddOtlpExporter(exporterOptions =>
                {
                    exporterOptions.Protocol = options.Protocol;
                    exporterOptions.ExportProcessorType = options.ExportProcessorType;
                }));
            builder.Services.ConfigureOpenTelemetryMeterProvider(metrics =>
                metrics.AddOtlpExporter(exporterOptions =>
                {
                    exporterOptions.Protocol = options.Protocol;
                    exporterOptions.ExportProcessorType = options.ExportProcessorType;
                }));
            builder.Services.ConfigureOpenTelemetryTracerProvider(tracing =>
                tracing.AddOtlpExporter(exporterOptions =>
                {
                    exporterOptions.Protocol = options.Protocol;
                    exporterOptions.ExportProcessorType = options.ExportProcessorType;
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