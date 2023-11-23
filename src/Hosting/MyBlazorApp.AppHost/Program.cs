using Microsoft.Extensions.Logging;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var backend = builder.AddProject<MyBlazorApp_Server_Backend>("backend")
    .WithLogLevel(LogLevel.Error)
    .WithAlias(MyBlazorApp.Services.DiscriminatedUnions.Contracts.V1.Endpoints.ServiceName)
    .WithAlias(MyBlazorApp.Services.WeatherForecasts.Contracts.V1.Endpoints.ServiceName);

builder.AddProject<MyBlazorApp_Server_BlazorServer>("blazorserver")
    .WithReferences(backend);

builder.AddProject<MyBlazorApp_Client_Photino>("photino")
    .WithReferences(backend);

builder.AddProject<MyBlazorApp_Server_WebAssemblyHost>("wasmhost")
    .WithReferences(backend)
    .WithWasmEnvironmentVariables();

builder.Build().Run();