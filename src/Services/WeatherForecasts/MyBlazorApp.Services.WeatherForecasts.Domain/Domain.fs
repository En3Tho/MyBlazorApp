namespace rec MyBlazorApp.Services.WeatherForecasts.Domain

open System
open Microsoft.Extensions.Logging
open MyBlazorApp.Utility.Logging.ILoggerExtensions
open En3Tho.FSharp.Extensions

type WeatherForecast = {
    Date: DateTime
    TemperatureC: int
    Summary: string
} with
    member x.TemperatureF = 32 + int (float x.TemperatureC / 0.5556)

module WeatherForecastsService =
    let private Summaries = [| "Freezing"; "Bracing"; "Chilly"; "Cool"; "Mild"; "Warm"; "Balmy"; "Hot"; "Sweltering"; "Scorching" |]

    let getForecasts (logger: ILogger) count =
        logger.Tracef $"{nameof getForecasts} called with parameters: {nameof count}={count}"

        let now = DateTime.Now
        let rng = Random()
        let result = [|
            for index in 0..count - 1 do {
                Date = now.AddDays(float index)
                TemperatureC = rng.Next(-20, 55)
                Summary = Summaries[rng.Next Summaries.Length]
            }
        |]
        result

    let getSuperForecasts (logger: ILogger) count superNumber =
        logger.Tracef $"{nameof getSuperForecasts} called with parameters: {nameof count}={count}, {nameof superNumber}={superNumber}"

        let forecasts = getForecasts logger count
        let forecasts = forecasts |> Array.map ^ fun forecast ->
            { forecast with Summary = $"{forecast.Summary}-SuperForecast-{superNumber}!" }
        forecasts