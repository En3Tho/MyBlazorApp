namespace MyBlazorApp.AppHost;

public static partial class DistributedApplicationBuilderExtensions
{
    // TODO: get link from app itself?
    public static IDistributedApplicationBuilder UseTelemetryProxyExporter(this IDistributedApplicationBuilder builder, IResourceBuilder<ProjectResource>[] resourceBuilders)
    {
        builder.ForAll(resourceBuilders, resourceBuilder =>
            resourceBuilder.WithEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT", "https://localhost:7198"));

        return builder;
    }

    public static IDistributedApplicationBuilder UseJaegerExporter(this IDistributedApplicationBuilder builder,
        IResourceBuilder<ProjectResource>[] resourceBuilders)
    {
        builder.ForAll(resourceBuilders, resourceBuilder =>
            resourceBuilder.WithEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT", "http://localhost:4317"));

        return builder;
    }

    public static IDistributedApplicationBuilder UseElasticExporter(this IDistributedApplicationBuilder builder,
        IResourceBuilder<ProjectResource>[] resourceBuilders)
    {
        builder.ForAll(resourceBuilders, resourceBuilder =>
            resourceBuilder.WithEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT", "http://localhost:8200"));

        return builder;
    }

    public static IDistributedApplicationBuilder UseSeqExporter(this IDistributedApplicationBuilder builder,
        IResourceBuilder<ProjectResource>[] resourceBuilders)
    {
        builder.ForAll(resourceBuilders, resourceBuilder =>
        {
            resourceBuilder
                .WithEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT", "http://localhost:5341/ingest/otlp")
                .WithEnvironmentVariable("OTEL_EXPORTER_OTLP_HEADERS", "X-Seq-ApiKey=jd4fexdTXU7VEdmvcFz3")
                .WithEnvironmentVariable("OTEL_EXPORTER_OTLP_PROTOCOL", "http/protobuf");
        });

        return builder;
    }
}