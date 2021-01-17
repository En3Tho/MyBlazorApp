namespace MyBlazorApp.Api.Console.FSharp

open ConsoleAppFramework
open Microsoft.Extensions.Logging
open MyBlazorApp.Api.Contracts
open MyBlazorApp.Domain

type WeatherForecastApiV1ConsoleClient(logger: WeatherForecastApiV1ConsoleClient ILogger) =
    inherit ConsoleAppBase()

    member this.GetForecasts count =
        WeatherForecastsService.getForecasts logger count

    member this.GetSuperForecasts count superNumber =
        WeatherForecastsService.getSuperForecasts logger count superNumber

    interface WeatherForecastApiV1.IContract with
        member this.GetForecasts count = this.GetForecasts count
        member this.GetSuperForecasts count superNumber = this.GetSuperForecasts count superNumber