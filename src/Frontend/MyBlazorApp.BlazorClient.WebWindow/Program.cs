using System;
using Photino.Blazor;

namespace MyBlazorApp.BlazorClient.WebWindow
{
    public class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            ComponentsDesktop.Run<Startup>(
                "MyBlazorApp.WebWindow",
                "wwwroot/index.html",
                x: 450,
                y: 100,
                width: 1000,
                height: 900);
        }
    }
}