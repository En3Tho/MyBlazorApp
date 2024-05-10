using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Microsoft.Extensions.Logging;

public static partial class DistributedApplicationBuilderExtensions
{
    public static IResourceBuilder<ProjectResource> WithAlias(this IResourceBuilder<ProjectResource> builder, string alias)
    {
        return builder.WithAnnotation(new EndpointAliasAnnotation(alias));
    }

    public static IResourceBuilder<TDestination> WithReferences<TDestination, U>(
        this IResourceBuilder<TDestination> destination,
        IResourceBuilder<U> source)
        where TDestination : IResourceWithEnvironment
        where U : IResource
    {
        static bool ContainsAmbiguousEndpoints(IEnumerable<EndpointAnnotation> endpoints)
        {
            // An ambiguous endpoint is where any scheme (
            return endpoints.GroupBy(e => e.UriScheme).Any(g => g.Count() > 1);
        }

        return destination.WithEnvironment(context =>
        {
            if (!source.Resource.TryGetEndpoints(out var allocatedEndPoints))
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
                        allocatedEndPoint.Name;

                    if (!containsAmbiguousEndpoints)
                    {
                        var uriStringKey = $"services__{name}__{i++}";
                        context.EnvironmentVariables[uriStringKey] = allocatedEndPoint.Name;
                    }
                }
            }
        });
    }

    public static IDistributedApplicationBuilder ForAll(this IDistributedApplicationBuilder builder, IResourceBuilder<ProjectResource>[] resourceBuilders,
        Action<IResourceBuilder<ProjectResource>> action)
    {
        foreach (var resourceBuilder in resourceBuilders)
        {
            action(resourceBuilder);
        }

        return builder;
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

    public static IResourceBuilder<T> WithLogLevel<T>(this IResourceBuilder<T> resourceBuilder,
        LogLevel defaultLogLevel, (string, LogLevel)[]? logLevels = null)
        where T : IResourceWithEnvironment
    {
        return resourceBuilder.WithLogLevel("", defaultLogLevel, logLevels);
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