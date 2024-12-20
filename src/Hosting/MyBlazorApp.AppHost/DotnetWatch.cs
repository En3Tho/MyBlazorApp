using Aspire.Dashboard.ConsoleLogs;

namespace MyBlazorApp.AppHost;

public static partial class DistributedApplicationBuilderExtensions
{
    public static IResourceBuilder<ExecutableResource> AddProjectWithDotnetWatch<TProject>(
        this IDistributedApplicationBuilder builder, string name) where TProject : IProjectMetadata, new()
    {
        var serviceMetadata = new TProject();
        var project =
            new ExecutableResource(name, "dotnet", Path.GetDirectoryName(serviceMetadata.ProjectPath)!);
        var executableBuilder = builder.AddResource(project);
        
        var options = new ProjectResourceOptions();
        
        executableBuilder.WithArgs([ "watch", "--non-interactive" ]);
        // We only want to turn these on for .NET projects, ConfigureOtlpEnvironment works for any resource type that
        // implements IDistributedApplicationResourceWithEnvironment.
        executableBuilder.WithEnvironment("OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EXCEPTION_LOG_ATTRIBUTES", "true");
        executableBuilder.WithEnvironment("OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EVENT_LOG_ATTRIBUTES", "true");
        executableBuilder.WithOtlpExporter();
        // executableBuilder.WithEnvironment(context =>
        // {
        //     if (context.PublisherName == "manifest")
        //     {
        //         return;
        //     }
        //
        //     // Enable ANSI Control Sequences for colors in Output Redirection
        //     context.EnvironmentVariables["DOTNET_SYSTEM_CONSOLE_ALLOW_ANSI_COLOR_REDIRECTION"] = "true";
        //
        //     // Enable Simple Console Logger Formatting with a UTC timestamp similar to RFC3339Nano that Docker generates
        //     context.EnvironmentVariables["LOGGING__CONSOLE__FORMATTERNAME"] = "simple";
        //     context.EnvironmentVariables["LOGGING__CONSOLE__FORMATTEROPTIONS__TIMESTAMPFORMAT"] =
        //         $"{TimestampParser.DisplayFormat} ";
        // });
        executableBuilder.WithAnnotation(serviceMetadata);
        return executableBuilder;
    }
}