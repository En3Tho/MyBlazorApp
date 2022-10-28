using System;
using Photino.Blazor;

namespace MyBlazorApp.BlazorClient.Photino;

public class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var builder = PhotinoBlazorAppBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("app");
        builder.Services.AddServices();
        var app = builder.Build();
        app.MainWindow.StartUrl = "index.html";
        app.Run();
    }
}