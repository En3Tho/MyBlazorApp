using System;
using Photino.Blazor;

namespace MyBlazorApp.BlazorClient.WebWindow;

public class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var app = PhotinoBlazorAppBuilder.CreateDefault(args);
        app.RootComponents.Add<App>("app");
        app.Services.AddServices();
        app.Build().Run();
    }
}