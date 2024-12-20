using Microsoft.Extensions.Logging;
using MyBlazorApp.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var backend = builder.AddProject<Projects.MyBlazorApp_Server_Backend>("srv")
    .WithAlias("discriminated-unions")
    .WithAlias("weather-forecasts");

var blazorserver = builder.AddProjectWithDotnetWatch<Projects.MyBlazorApp_Server_BlazorServer>("blazorserver");

var photino = builder.AddProject<Projects.MyBlazorApp_Client_Photino>("photino");

var wasmhost = builder.AddProject<Projects.MyBlazorApp_Server_WebAssemblyHost>("wasmhost");

var telemetryProxy = builder.AddProject<Projects.TelemetryProxy>("telemetry-proxy", "https");

builder
    .ForAll([ backend, photino, wasmhost, telemetryProxy ], resourceBuilder =>
        resourceBuilder.WithLogLevel(LogLevel.Information))
    .ForAll([ blazorserver ], resourceBuilder =>
        resourceBuilder.WithLogLevel(LogLevel.Information))

    .ForAll([ photino, wasmhost ], resourceBuilder =>
        resourceBuilder
            .WithReferences(backend))
    .ForAll([ blazorserver ], resourceBuilder =>
        resourceBuilder
            .WithReferences(backend));

// builder.UseElasticExporter([ backend, blazorserver, photino, wasmhost ]);
// builder.UseTelemetryProxy([backend, blazorserver, photino, wasmhost]);

wasmhost
    .WithEnvironmentVariable("WASM__OTEL_SERVICE_NAME", "wasm")
    .WithLogLevel("WASM__", LogLevel.Information)
    .WithEnvironmentCopy("WASM__");

builder.Build().Run();