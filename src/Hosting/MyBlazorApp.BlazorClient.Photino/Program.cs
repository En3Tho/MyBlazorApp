using System;
using Microsoft.Extensions.Configuration;
using MyBlazorApp.ComponentsAndPages.Shared;
using Photino.Blazor;

namespace MyBlazorApp.BlazorClient.Photino;

public class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var builder = PhotinoBlazorAppBuilder.CreateDefault(args);
        var confBuilder = new ConfigurationBuilder();
        confBuilder.AddJsonFile("wwwroot/appsettings.json");
        var configuration = confBuilder.Build();
        builder.RootComponents.Add<App>("app");
        builder.Services.AddServices(configuration);
        var app = builder.Build();
        app.MainWindow.SetLogVerbosity(0);
        app.Run();
    }
}