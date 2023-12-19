using Microsoft.Extensions.Logging;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var backend = builder.AddProject<MyBlazorApp_Server_Backend>("srv")
    .WithAlias(MyBlazorApp.Services.DiscriminatedUnions.Contracts.V1.Endpoints.ServiceName)
    .WithAlias(MyBlazorApp.Services.WeatherForecasts.Contracts.V1.Endpoints.ServiceName);

var blazorserver = builder.AddProject<MyBlazorApp_Server_BlazorServer>("blazorserver");

var photino = builder.AddProject<MyBlazorApp_Client_Photino>("photino");

var wasmhost = builder.AddProject<MyBlazorApp_Server_WebAssemblyHost>("wasmhost");

builder.ForAll([backend, blazorserver, photino, wasmhost], resourceBuilder =>
    // resourceBuilder.WithLogLevel(LogLevel.Trace, [("Microsoft", LogLevel.Error)]))
    resourceBuilder.WithLogLevel(LogLevel.Trace))
        .ForAll([blazorserver, photino, wasmhost], resourceBuilder =>
    resourceBuilder.WithReferences(backend));

// // ElasticAPM
// builder.ForAll([backend, blazorserver, photino, wasmhost], resourceBuilder =>
//     resourceBuilder.WithEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT", "http://localhost:8200"));

// Seq
builder.ForAll([backend, blazorserver, photino, wasmhost], resourceBuilder =>
{
    resourceBuilder
        .WithEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT", "http://localhost:5341/ingest/otlp")
        .WithEnvironmentVariable("OTEL_EXPORTER_OTLP_HEADERS", "X-Seq-ApiKey=jd4fexdTXU7VEdmvcFz3")
        .WithEnvironmentVariable("OTEL_EXPORTER_OTLP_PROTOCOL", "http/protobuf");
});

wasmhost
    .WithEnvironmentVariable("WASM__OTEL_SERVICE_NAME", "wasm")
    .WithLogLevel("WASM__", LogLevel.Error)
    .WithEnvironmentVariablesCopy("WASM__");

builder.Build().Run();