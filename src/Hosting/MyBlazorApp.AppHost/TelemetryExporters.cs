using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace MyBlazorApp.AppHost;

public static partial class DistributedApplicationBuilderExtensions
{
    public static IDistributedApplicationBuilder UseElastic(this IDistributedApplicationBuilder builder,
        IResourceBuilder<ProjectResource>[] resourceBuilders)
    {
        builder.ForAll(resourceBuilders, resourceBuilder =>
            resourceBuilder.WithEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT", "http://localhost:8200"));

        return builder;
    }

    public static IDistributedApplicationBuilder UseSeq(this IDistributedApplicationBuilder builder,
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