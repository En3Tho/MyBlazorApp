namespace MyBlazorApp.BlazorClient.Maui
{
    public interface INotificationService
    {
        void ShowNotification(string title, string subtitle, string body);
    }
}
