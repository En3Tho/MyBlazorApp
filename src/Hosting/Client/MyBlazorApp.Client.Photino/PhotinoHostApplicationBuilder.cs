using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.Metrics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Photino.Blazor;

namespace MyBlazorApp.Client.Photino;

class PhotinoHostApplicationBuilder : IHostApplicationBuilder
{
    private readonly HostApplicationBuilder hostApplicationBuilder;

    public PhotinoHostApplicationBuilder(string[]? args)
    {
        hostApplicationBuilder = new(args);
        Services.AddBlazorDesktop();
        Services.AddBlazorWebView();
    }

    public (IHost, PhotinoBlazorApp) Build(Action<IServiceProvider>? serviceProviderOptions = null)
    {
        var host = hostApplicationBuilder.Build(); // what to do with host?

        PhotinoBlazorApp photinoBlazorApp = host.Services.GetRequiredService<PhotinoBlazorApp>();
        serviceProviderOptions?.Invoke(host.Services);

        photinoBlazorApp
            .GetType()
            .GetMethod("Initialize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .Invoke(photinoBlazorApp, [ host.Services, RootComponents ]);

        return (host, photinoBlazorApp);
    }

    public void ConfigureContainer<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory, Action<TContainerBuilder>? configure = null) where TContainerBuilder : notnull
    {
        hostApplicationBuilder.ConfigureContainer(factory, configure);
    }

    public RootComponentList RootComponents { get; } = new ();

    public IDictionary<object, object> Properties => ((IHostApplicationBuilder)hostApplicationBuilder).Properties;
    public IConfigurationManager Configuration => hostApplicationBuilder.Configuration;
    public IHostEnvironment Environment => hostApplicationBuilder.Environment;
    public ILoggingBuilder Logging => hostApplicationBuilder.Logging;
    public IMetricsBuilder Metrics => hostApplicationBuilder.Metrics;
    public IServiceCollection Services => hostApplicationBuilder.Services;
}