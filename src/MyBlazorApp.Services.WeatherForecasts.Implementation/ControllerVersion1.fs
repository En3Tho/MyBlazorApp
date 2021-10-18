namespace MyBlazorApp.Services.WeatherForecasts.Hosting

open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open MyBlazorApp.Services.WeatherForecasts.Contracts.Version1
open MyBlazorApp.Services.WeatherForecasts.CrossCutting
open MyBlazorApp.Services.WeatherForecasts.Domain

type WeatherForecastServiceVersion1(logger: WeatherForecastServiceVersion1 ILogger) =

    member this.GetForecasts count =
        WeatherForecastsService.getForecasts logger count
        |> Array.map WeatherForecast.toDto
        |> ValueTask.FromResult

    member this.GetSuperForecasts count superNumber =
        WeatherForecastsService.getSuperForecasts logger count superNumber
        |> Array.map WeatherForecast.toDto
        |> ValueTask.FromResult

    interface IWeatherForecastsService with
        member this.GetForecasts count = this.GetForecasts count
        member this.GetSuperForecasts (count, superNumber) = this.GetSuperForecasts count superNumber

[<Route(Routes.ServiceName)>]
type WeatherForecastsServiceVersion1Controller(service: IWeatherForecastsService, logger: ILogger<WeatherForecastsServiceVersion1Controller>) =
    inherit ControllerBase()

    [<HttpGet(Routes.GetForecasts)>]
    member _.GetForecasts (count: int) =
        service.GetForecasts(count)

    [<HttpGet(Routes.GetSuperForecasts)>]
    member _.GetSuperForecasts (count: int, superNumber: int) =
        service.GetSuperForecasts(count, superNumber)