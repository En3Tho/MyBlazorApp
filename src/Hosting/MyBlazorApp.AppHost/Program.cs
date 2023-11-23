using Microsoft.Extensions.Logging;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var backend = builder.AddProject<MyBlazorApp_Server_Backend>("srv")
    .WithAlias(MyBlazorApp.Services.DiscriminatedUnions.Contracts.V1.Endpoints.ServiceName)
    .WithAlias(MyBlazorApp.Services.WeatherForecasts.Contracts.V1.Endpoints.ServiceName);

var blazorserver = builder.AddProject<MyBlazorApp_Server_BlazorServer>("blazorserver")
    .WithReferences(backend);

var photino = builder.AddProject<MyBlazorApp_Client_Photino>("photino")
    .WithReferences(backend);

var wasmhost = builder.AddProject<MyBlazorApp_Server_WebAssemblyHost>("wasmhost")
    .WithReferences(backend);

DistributedApplicationBuilderExtensions.All([backend, blazorserver, photino, wasmhost],
    builder => builder.WithLogLevel("", LogLevel.Trace, [
        ("Microsoft", LogLevel.Error),
        ("Microsoft.AspNetCore", LogLevel.Information)
    ]));

wasmhost
    .WithEnvironmentVariable("WASM__OTEL_SERVICE_NAME", "wasm")
    .WithLogLevel("WASM__", LogLevel.Error)
    .WithEnvironmentVariablesCopy("WASM__");

builder.Build().Run();