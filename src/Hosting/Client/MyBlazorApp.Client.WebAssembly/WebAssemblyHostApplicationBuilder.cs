using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Diagnostics.Metrics;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using MyBlazorApp.ComponentsAndPages.Shared;

class WebAssemblyHostApplicationBuilder : IHostApplicationBuilder
{
    class WebAssemblyMetricsBuilder(IServiceCollection serviceCollection) : IMetricsBuilder
    {
        public IServiceCollection Services => serviceCollection;
    }

    class WebAssemblyHostEnvironment(IWebAssemblyHostEnvironment hostEnvironment) : IHostEnvironment
    {
        public string EnvironmentName { get; set; } = hostEnvironment.Environment;
        public string ApplicationName { get; set; } = "MyBlazorApp";
        public string ContentRootPath { get; set; } = "/";
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
    }

    class WebAssemblyHostConfigurationManager(WebAssemblyHostConfiguration configuration) : IConfigurationManager
    {
        public IConfigurationSection GetSection(string key) => configuration.GetSection(key);

        public IEnumerable<IConfigurationSection> GetChildren() => ((IConfiguration)configuration).GetChildren();

        public IChangeToken GetReloadToken() => configuration.GetReloadToken();

        public string? this[string key]
        {
            get => configuration[key];
            set => configuration[key] = value;
        }

        public IConfigurationBuilder Add(IConfigurationSource source) => configuration.Add(source);

        public IConfigurationRoot Build() => configuration.Build();

        public IDictionary<string, object> Properties => ((IConfigurationBuilder)configuration).Properties;
        public IList<IConfigurationSource> Sources => ((IConfigurationBuilder)configuration).Sources;
    }

    private readonly WebAssemblyHostBuilder _builder;

    public WebAssemblyHostApplicationBuilder(string[] args)
    {
        _builder = WebAssemblyHostBuilder.CreateDefault(args);

        _builder.RootComponents.Add<App>("#app");
        _builder.RootComponents.Add<HeadOutlet>("head::after");

        Environment = new WebAssemblyHostEnvironment(_builder.HostEnvironment);
        Configuration = new WebAssemblyHostConfigurationManager(_builder.Configuration);
        Metrics = new WebAssemblyMetricsBuilder(_builder.Services);
    }

    public IWebAssemblyHostEnvironment HostEnvironment => _builder.HostEnvironment;

    public void ConfigureContainer<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory, Action<TContainerBuilder>? configure = null) where TContainerBuilder : notnull
    {
        _builder.ConfigureContainer(factory, configure);
    }

    public WebAssemblyHost Build() => _builder.Build();

    public IDictionary<object, object> Properties { get; } = new Dictionary<object, object>();
    public IConfigurationManager Configuration { get; }
    public IHostEnvironment Environment { get; }
    public ILoggingBuilder Logging => _builder.Logging;
    public IMetricsBuilder Metrics { get; }
    public IServiceCollection Services => _builder.Services;
}