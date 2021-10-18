using System;

namespace MyBlazorApp.BlazorClient.Maui
{
    public interface ITrayService
    {
        void Initialize();

        Action ClickHandler { get; set; }
    }
}
