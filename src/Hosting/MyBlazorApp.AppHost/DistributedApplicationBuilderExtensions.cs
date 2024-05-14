using Microsoft.Extensions.Logging;

public static partial class DistributedApplicationBuilderExtensions
{
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

    public static IResourceBuilder<T> WithEnvironmentCopy<T>(
        this IResourceBuilder<T> project, string prefix)
        where T : IResourceWithEnvironment
    {
        return project.WithEnvironment(context =>
        {
            var envs = new Dictionary<string, object>(context.EnvironmentVariables);
            foreach (var env in envs)
            {
                if (env.Key.StartsWith(prefix))
                {
                    continue;
                }

                var prefixedKey = $"{prefix}{env.Key}";
                context.EnvironmentVariables.TryAdd(prefixedKey, env.Value);
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