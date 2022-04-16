using Hardcodet.Wpf.TaskbarNotification.Interop;
using System;
using MyBlazorApp.BlazorClient.Maui.Services;

namespace MyBlazorApp.BlazorClient.Maui.WinUI
{
    public class TrayService : ITrayService
    {
        WindowsTrayIcon tray;

        public Action ClickHandler { get; set; }

        public void Initialize()
        {
            tray = new WindowsTrayIcon("Platforms/Windows/Resources/trayicon.ico")
            {
                LeftClick = () =>
                {                   
                    Microsoft.Maui.MauiWinUIApplication.Current.Application.Windows[0].BringToFront();
                    ClickHandler?.Invoke();
                }
            };
        }
    }
}
