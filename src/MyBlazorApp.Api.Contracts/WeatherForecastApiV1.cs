using System.Threading;
using System.Threading.Tasks;
using MyBlazorApp.Domain.WeatherForecasts;

namespace MyBlazorApp.Api.Contracts
{
    public static class WeatherForecastApiV1
    {
        public const string Version = "v1";
        public static class Routes
        {
            public const string Controller = Version + "/" + "weatherforecasts";
            public const string GetForecasts = "";
            public const string GetSuperForecasts = "superforecasts";
        }

        public static class Endpoints
        {
            public const string GetForecasts = Routes.Controller + "/" + Routes.GetForecasts;
            public const string GetSuperForecasts = Routes.Controller + "/" + Routes.GetSuperForecasts + "/";
        }

        public interface Interface
        {
            public Task<WeatherForecast[]> GetForecasts(int count, CancellationToken cancellationToken = default);
            public Task<WeatherForecast[]> GetSuperForecasts(int count, int superNumber, CancellationToken cancellationToken = default);
        }
    }
}