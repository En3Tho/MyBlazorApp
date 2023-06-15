﻿using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlazorApp.ComponentsAndPages.Shared;
using Photino.Blazor;

namespace MyBlazorApp.BlazorClient.Photino;

public class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var configurationManager = new ConfigurationManager();
        configurationManager.AddJsonFile("wwwroot/appsettings.json");

        var builder = PhotinoBlazorAppBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("app");
        builder.Services.AddServices(configurationManager);
        builder.Services.AddSingleton(configurationManager);

        var app = builder.Build();
        app.MainWindow.SetLogVerbosity(0);
        app.Run();
    }
}