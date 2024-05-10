using Microsoft.Extensions.Logging;
using MyBlazorApp.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var backend = builder.AddProject<Projects.MyBlazorApp_Server_Backend>("srv")
    .WithAlias("discriminated-unions")
    .WithAlias("weather-forecasts");

var blazorserver = builder.AddProject<Projects.MyBlazorApp_Server_BlazorServer>("blazorserver");

var photino = builder.AddProject<Projects.MyBlazorApp_Client_Photino>("photino");

var wasmhost = builder.AddProject<Projects.MyBlazorApp_Server_WebAssemblyHost>("wasmhost");

builder
    .ForAll([backend, blazorserver, photino, wasmhost], resourceBuilder =>
    // resourceBuilder.WithLogLevel(LogLevel.Trace, [("Microsoft", LogLevel.Error)]))
        resourceBuilder.WithLogLevel(LogLevel.Trace))
    .ForAll([blazorserver, photino, wasmhost], resourceBuilder =>
        resourceBuilder.WithReferences(backend));

// ElasticAPM
// builder.UseElastic([backend, blazorserver, photino, wasmhost]);

// Seq
// builder.UseSeq([backend, blazorserver, photino, wasmhost]);

wasmhost
    .WithEnvironmentVariable("WASM__OTEL_SERVICE_NAME", "wasm")
    .WithLogLevel("WASM__", LogLevel.Error)
    .WithEnvironmentCopy("WASM__");

builder.Build().Run();