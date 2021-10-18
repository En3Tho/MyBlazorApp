using System;
using static MyBlazorApp.Services.WeatherForecasts.Contracts.Version1;

namespace MyBlazorApp.ComponentsAndPages.Pages.WeatherForecasts
{
    public record WeatherForecast(DateTime Date, int TemperatureC, string Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public static WeatherForecast FromDto(WeatherForecastDto dto) => new(dto.Date, dto.TemperatureC, dto.Summary);
    }
}