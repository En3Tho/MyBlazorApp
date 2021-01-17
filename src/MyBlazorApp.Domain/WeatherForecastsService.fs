module MyBlazorApp.Domain.WeatherForecastsService

open System
open System.Threading.Tasks
open Microsoft.Extensions.Logging
open FSharp.Control.Tasks.V2
open MyBlazorApp.Utility.Logging.ILoggerExtensions
open MyBlazorApp.Utility.Modules.Core

[<CLIMutable>]
type WeatherForecast = {
        Date: DateTime
        TemperatureC: int
        Summary: string
    }
    with member x.TemperatureF = 32 + int (float x.TemperatureC / 0.5556)

let private Summaries = [| "Freezing"; "Bracing"; "Chilly"; "Cool"; "Mild"; "Warm"; "Balmy"; "Hot"; "Sweltering"; "Scorching" |]

let rec getForecasts (logger: ILogger) count =
    logger.Tracef $"{nameof getForecasts} called with parameters: {nameof count}={count}"

    let now = DateTime.Now
    let rng = Random(int now.Ticks)
    let result = [|
        for index in 0..count - 1 do {
            Date = now.AddDays(float index)
            TemperatureC = rng.Next(-20, 55)
            Summary = Summaries.[rng.Next Summaries.Length]
        }
    |]
    Task.FromResult result

let rec getSuperForecasts (logger: ILogger) count superNumber = task {
    logger.Tracef $"{nameof getSuperForecasts} called with parameters: {nameof count}={count}, {nameof superNumber}={superNumber}"

    let! forecasts = getForecasts logger count
    let forecasts = forecasts |> Array.map ^ fun forecast ->
        { forecast with Summary = $"{forecast.Summary}-SuperForecast-{superNumber}!" }
    return forecasts
}