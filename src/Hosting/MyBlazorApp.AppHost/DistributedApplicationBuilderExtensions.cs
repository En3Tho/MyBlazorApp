using Aspire.Dashboard.ConsoleLogs;
using Microsoft.Extensions.Logging;

public static class DistributedApplicationBuilderExtensions
{
    public static IResourceBuilder<ExecutableResource> AddProjectWithDotnetWatch<TProject>(
        this IDistributedApplicationBuilder builder, string name) where TProject : IServiceMetadata, new()
    {
        var serviceMetadata = new TProject();
        var project =
            new ExecutableResource(name, "dotnet", Path.GetDirectoryName(serviceMetadata.ProjectPath)!, ["watch", "--non-interactive"]);
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

            string[] aliases =  [source.Resource.Name, ..source.Resource.Annotations.OfType<EndpointAliasAnnotation>().Select(a => a.Alias)];

            foreach (var name in aliases.Distinct())
            {
                var i = 0;
                foreach (var allocatedEndPoint in allocatedEndPoints)
                {
                    var bindingNameQualifiedUriStringKey = $"services__{name}__{i++}";
                    context.EnvironmentVariables[bindingNameQualifiedUriStringKey] =
                        allocatedEndPoint.BindingNameQualifiedUriString;

                    if (!containsAmbiguousEndpoints)
                    {
                        var uriStringKey = $"services__{name}__{i++}";
                        context.EnvironmentVariables[uriStringKey] = allocatedEndPoint.UriString;
                    }
                }
            }
        });
    }

    public static void All(IResourceBuilder<ProjectResource>[] resourceBuilders,
        Action<IResourceBuilder<ProjectResource>> action)
    {
        foreach (var resourceBuilder in resourceBuilders)
        {
            action(resourceBuilder);
        }
    }

    public static IResourceBuilder<T> WithLogLevel<T>(this IResourceBuilder<T> resourceBuilder, string prefix,
        LogLevel defaultLogLevel, (string, LogLevel)[]? logLevels = null)
        where T : IResourceWithEnvironment
    {
        return resourceBuilder.WithEnvironment(context =>
        {
            context.EnvironmentVariables["Logging__LogLevel__Default"] = defaultLogLevel.ToString();
            if (logLevels is {})
            {
                foreach (var (tag, logLevel) in logLevels)
                {
                    context.EnvironmentVariables[$"Logging__LogLevel__{tag}"] = logLevel.ToString();
                }
            }
        });
    }

    public static IResourceBuilder<ProjectResource> WithAlias(this IResourceBuilder<ProjectResource> builder,
        string alias)
    {
        return builder.WithAnnotation(new EndpointAliasAnnotation(alias));
    }

    public static IResourceBuilder<T> WithEnvironmentVariablesCopy<T>(
        this IResourceBuilder<T> project, string prefix)
        where T : IResourceWithEnvironment
    {
        return project.WithEnvironment(context =>
        {
            var envs = new Dictionary<string, string>(context.EnvironmentVariables);
            foreach (var env in envs)
            {
                var prefixedKey = $"{prefix}{env.Key}";
                if (env.Key.StartsWith(prefix) || context.EnvironmentVariables.ContainsKey(prefixedKey))
                {
                    continue;
                }
                context.EnvironmentVariables[prefixedKey] = env.Value;
            }
        });
    }

    public static IResourceBuilder<T> WithEnvironmentVariable<T>(
        this IResourceBuilder<T> project, string key, string value)
        where T : IResourceWithEnvironment
    {
        return project.WithEnvironment(context => { context.EnvironmentVariables[key] = value; });
    }
}