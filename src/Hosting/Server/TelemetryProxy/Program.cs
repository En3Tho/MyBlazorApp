using TelemetryProxy;
using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<TelemetryProxyMiddleware>();

var traces = new RouteConfig
{
    RouteId = "Traces",
    ClusterId = "TelemetryExportersCluster",
    Match = new()
    {
        Path = "/opentelemetry.proto.collector.trace.v1.TraceService/Export",
        Methods = ["POST"]
    }
};

var logs = new RouteConfig
{
    RouteId = "Logs",
    ClusterId = "TelemetryExportersCluster",
    Match = new()
    {
        Path = "/opentelemetry.proto.collector.logs.v1.LogsService/Export",
        Methods = ["POST"]
    }
};

var metrics = new RouteConfig
{
    RouteId = "Metrics",
    ClusterId = "TelemetryExportersCluster",
    Match = new()
    {
        Path = "/opentelemetry.proto.collector.metrics.v1.MetricsService/Export",
        Methods = ["POST"]
    }
};

// clusters can be abstracted to return a stream and a task for completion?
var clusters = new ClusterConfig
{
    ClusterId = "TelemetryExportersCluster",
    Destinations = new Dictionary<string, DestinationConfig>
    {
        ["Jaeger"] = new()
        {
            Address = "http://localhost:4317"
        },
        ["Elastic"] = new()
        {
            Address = "http://localhost:8200"
        },
        // ["Seq"] = new()
        // {
        //     Address = "http://localhost:5341/ingest/otlp",
        //     Metadata = new Dictionary<string, string>
        //     {
        //         ["X-Seq-ApiKey"] = "X-Seq-ApiKey=jd4fexdTXU7VEdmvcFz3"
        //     }
        // },
        ["Dashboard"] = new()
        {
            Address = Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT")!
        }
    },
    HttpRequest = new()
    {
        Version = new(2, 0),
        VersionPolicy = HttpVersionPolicy.RequestVersionExact
    }
};

var app = builder.Build();

app.UseMiddleware<TelemetryProxyMiddleware>();

app.Run();

class TelemetryProxyMiddleware(RequestDelegate next, ILogger<TelemetryProxyMiddleware> logger, HttpClient client)
{
    public async Task Invoke(HttpContext context)
    {
        var request = context.Request;
        Task? task = null;
        if (request is { Method: "POST", ContentType: "application/grpc" })
        {
            if (request.Path == "/opentelemetry.proto.collector.trace.v1.TraceService/Export")
            {
                task = Proxy.MirrorRequest("/opentelemetry.proto.collector.trace.v1.TraceService/Export", context, client, logger);
            }
            else if (request.Path == "/opentelemetry.proto.collector.logs.v1.LogsService/Export")
            {
                task = Proxy.MirrorRequest("/opentelemetry.proto.collector.logs.v1.LogsService/Export", context, client, logger);
            }
            else if (request.Path == "/opentelemetry.proto.collector.metrics.v1.MetricsService/Export")
            {
                task = Proxy.MirrorRequest("/opentelemetry.proto.collector.metrics.v1.MetricsService/Export", context, client, logger);
            }
        }

        await (task ?? next(context));
    }
}

// HttpRequestMessageFactory: ...
// Host, headers, whatever
// ...

// In theory, for each exporter a stream can be opened and kept alive for some time?
// then reuse that stream by utilizing a channel/query?
class HostProxy(HttpClient client, ILogger<HostProxy> logger)
{
    public async Task SendAsync(HttpRequestMessage msg,  CancellationTokenSource cts)
    {
        try
        {
            await client.SendAsync(msg, cts.Token);
        }
        catch (Exception ex)
        {
            cts.Cancel();
            logger.LogError(ex, null);
        }
    }
}

static class Proxy
{
    static readonly Version Http2Version = new(2, 0);
    static readonly Version Http11Version = new(1, 1);

    private static readonly string[] hosts =
        [
            "http://localhost:4317",
            Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT")! // dashboard
            // "http://localhost:8200"
        ];

    private static async Task SendRequest(HttpClient client, HttpRequestMessage msg, ILogger logger, CancellationToken cancellationToken)
    {
        try
        {
            await client.SendAsync(msg, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Boom");
        }
    }

    // broadcast stream only writes
    // read a part of the stream and write to multiple streams (each stream can have a channel to store byte chunks?)

    public static async Task MirrorRequest(string path, HttpContext context, HttpClient client, ILogger logger)
    {
        var originalStream = context.Request.Body;
        var proxyStreams = new List<ProxyStream>();

        foreach (var host in hosts)
        {
            var proxyStream = new ProxyStream();
            proxyStreams.Add(proxyStream);

            var msg = new HttpRequestMessage(HttpMethod.Post, host + path)
            {
                Version = Http2Version,
                Content = new StreamContent(proxyStream) { Headers = { ContentType = new("application/grpc") } },
                VersionPolicy = HttpVersionPolicy.RequestVersionExact
            };

            foreach (var header in context.Request.Headers)
            {
                foreach (var value in header.Value)
                {
                    msg.Headers.TryAddWithoutValidation(header.Key, value);
                }
            }

            _ = SendRequest(client, msg, logger, context.RequestAborted);
        }

        var buffer = new byte[1024];
        while (true)
        {
            var bytesRead = await originalStream.ReadAsync(buffer, context.RequestAborted);
            if (bytesRead == 0)
            {
                foreach (var stream in proxyStreams)
                {
                    stream.Dispose();
                }
                break;
            }

            foreach (var stream in proxyStreams)
            {
                _ = stream.WriteAsync(buffer.AsMemory(0, bytesRead), context.RequestAborted);
            }
        }
    }
}