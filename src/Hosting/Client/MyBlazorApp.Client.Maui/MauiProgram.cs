using Kotlin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.Metrics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Storage;

namespace MyBlazorApp.Client.Maui;

class MauiAppBuilderWrapper(MauiAppBuilder builder) : IHostApplicationBuilder
{
    public void ConfigureContainer<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory, Action<TContainerBuilder>? configure = null) where TContainerBuilder : notnull
    {
        throw new NotImplementedException();
    }

    public IDictionary<object, object> Properties => throw new NotImplementedException();
    public IConfigurationManager Configuration => builder.Configuration;
    public IHostEnvironment Environment => throw new NotImplementedException();
    public ILoggingBuilder Logging => builder.Logging;
    public IMetricsBuilder Metrics => throw new NotImplementedException();
    public IServiceCollection Services => builder.Services;

    public MauiAppBuilder Builder => builder;
    public MauiApp Build() => builder.Build();
}

public static class MauiProgram
{
    public static async Task<MauiApp> CreateMauiApp()
    {
        var builder = new MauiAppBuilderWrapper(MauiApp.CreateBuilder());
        builder.Builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.AddClientDefaults();
        // builder.ConfigureOpenTelemetry(new());
        builder.Configuration.AddJsonStream(await FileSystem.OpenAppPackageFileAsync("wwwroot/appsettings.json"));
        builder.Services.AddMauiBlazorWebView();

        return builder.Build();
    }
}