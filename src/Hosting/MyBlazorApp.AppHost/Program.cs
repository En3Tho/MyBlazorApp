using Microsoft.Extensions.Logging;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var backend = builder.AddProject<MyBlazorApp_Server_Backend>("srv")
    .WithAlias("discriminated-unions")
    .WithAlias("weather-forecasts");

var blazorserver = builder.AddProject<MyBlazorApp_Server_BlazorServer>("blazorserver");

var photino = builder.AddProject<MyBlazorApp_Client_Photino>("photino");

var wasmhost = builder.AddProject<MyBlazorApp_Server_WebAssemblyHost>("wasmhost");

builder
    .ForAll([backend, blazorserver, photino, wasmhost], resourceBuilder =>
    // resourceBuilder.WithLogLevel(LogLevel.Trace, [("Microsoft", LogLevel.Error)]))
        resourceBuilder.WithLogLevel(LogLevel.Trace))
    .ForAll([blazorserver, photino, wasmhost], resourceBuilder =>
        resourceBuilder.WithReferences(backend));

// ElasticAPM
//builder.UseElastic([backend, blazorserver, photino, wasmhost]);

// Seq
// builder.UseSeq([backend, blazorserver, photino, wasmhost]);

wasmhost
    .WithEnvironmentVariable("WASM__OTEL_SERVICE_NAME", "wasm")
    .WithLogLevel("WASM__", LogLevel.Error)
    .WithEnvironmentVariablesCopy("WASM__");

builder.Build().Run();