using System;

namespace MyBlazorApp.BlazorClient.Maui.Services
{
    public interface ITrayService
    {
        void Initialize();

        Action ClickHandler { get; set; }
    }
}
