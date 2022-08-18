using Application = Microsoft.Maui.Controls.Application;

namespace MyBlazorApp.BlazorClient.Maui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new MainPage() { Title = "My Blazor App" };
    }
}