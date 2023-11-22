using Aspire.Dashboard.ConsoleLogs;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var backend = builder.AddProject<MyBlazorApp_Server_Backend>("backend")
    .WithAlias(MyBlazorApp.Services.DiscriminatedUnions.Contracts.V1.Endpoints.ServiceName)
    .WithAlias(MyBlazorApp.Services.WeatherForecasts.Contracts.V1.Endpoints.ServiceName);

builder.AddProject<MyBlazorApp_Server_BlazorServer>("blazorserver")
    .WithReferences(backend);

builder.AddProject<MyBlazorApp_Client_Photino>("photino")
    .WithReferences(backend);

builder.AddProject<MyBlazorApp_Client_WebAssembly>("wasm")
    .WithReferences(backend);

builder.Build().Run();

public static class DistributedApplicationBuilderExtensions
{
    public static IResourceBuilder<ExecutableResource> AddProjectWithDotnetWatch<TProject>(this IDistributedApplicationBuilder builder, string name) where TProject : IServiceMetadata, new()
    {
        var serviceMetadata = new TProject();
        var project = new ExecutableResource(name, "dotnet", Path.GetDirectoryName(serviceMetadata.ProjectPath)!, ["watch", "--non-interactive"]);
        var executableBuilder = builder.AddResource(project);
        // We only want to turn these on for .NET projects, ConfigureOtlpEnvironment works for any resource type that
        // implements IDistributedApplicationResourceWithEnvironment.
        executableBuilder.WithEnvironment("OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EXCEPTION_LOG_ATTRIBUTES", "true");
        executableBuilder.WithEnvironment("OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EVENT_LOG_ATTRIBUTES", "true");
        executableBuilder.WithOtlpExporter();
        executableBuilder.WithEnvironment((context) =>
        {
            if (context.PublisherName == "manifest")
            {
                return;
            }

            // Enable ANSI Control Sequences for colors in Output Redirection
            context.EnvironmentVariables["DOTNET_SYSTEM_CONSOLE_ALLOW_ANSI_COLOR_REDIRECTION"] = "true";

            // Enable Simple Console Logger Formatting with a UTC timestamp similar to RFC3339Nano that Docker generates
            context.EnvironmentVariables["LOGGING__CONSOLE__FORMATTERNAME"] = "simple";
            context.EnvironmentVariables["LOGGING__CONSOLE__FORMATTEROPTIONS__TIMESTAMPFORMAT"] =
                $"{TimestampParser.DisplayFormat} ";
        });
        executableBuilder.WithAnnotation(serviceMetadata);
        return executableBuilder;
    }

    public static IResourceBuilder<TDestination> WithReferences<TDestination, U>(
        this IResourceBuilder<TDestination> destination,
        IResourceBuilder<U> source)
        where TDestination : IResourceWithEnvironment
        where U : IResource
    {
        static bool ContainsAmbiguousEndpoints(IEnumerable<AllocatedEndpointAnnotation> endpoints)
        {
            // An ambiguous endpoint is where any scheme (
            return endpoints.GroupBy(e => e.UriScheme).Any(g => g.Count() > 1);
        }

        return destination.WithEnvironment(context =>
        {
            if (!source.Resource.TryGetAllocatedEndPoints(out var allocatedEndPoints))
            {
                return;
            }

            var containsAmbiguousEndpoints = ContainsAmbiguousEndpoints(allocatedEndPoints);

            string[] aliases = [source.Resource.Name, ..source.Resource.Annotations.OfType<EndpointAliasAnnotation>().Select(a => a.Alias)];

            foreach (var name in aliases.Distinct())
            {
                var i = 0;
                foreach (var allocatedEndPoint in allocatedEndPoints)
                {
                    var bindingNameQualifiedUriStringKey = $"services__{name}__{i++}";
                    context.EnvironmentVariables[bindingNameQualifiedUriStringKey] = allocatedEndPoint.BindingNameQualifiedUriString;

                    if (!containsAmbiguousEndpoints)
                    {
                        var uriStringKey = $"services__{name}__{i++}";
                        context.EnvironmentVariables[uriStringKey] = allocatedEndPoint.UriString;
                    }
                }
            }
        });
    }

    public static IResourceBuilder<ProjectResource> WithAlias(this IResourceBuilder<ProjectResource> builder, string alias)
    {
        return builder.WithAnnotation(new EndpointAliasAnnotation(alias));
    }
}

class EndpointAliasAnnotation(string alias) : IResourceAnnotation
{
    public string Alias => alias;
}