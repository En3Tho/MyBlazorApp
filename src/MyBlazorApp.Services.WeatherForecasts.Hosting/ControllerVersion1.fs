namespace MyBlazorApp.Services.WeatherForecasts.Controller

open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open MyBlazorApp.Services.WeatherForecasts.Contracts
open MyBlazorApp.Services.WeatherForecasts.Domain

[<Route(Version1.Routes.ServiceName)>]
type WeatherForecastsServiceVersion1Controller(logger: ILogger<WeatherForecastsServiceVersion1Controller>) =
   inherit ControllerBase()

   [<HttpGet(Version1.Routes.GetForecasts)>]
   member _.GetForecasts (count: int) =
      WeatherForecastsService.getForecasts logger count
      |> Task.FromResult

   [<HttpGet(Version1.Routes.GetSuperForecasts)>]
   member _.GetSuperForecasts (count: int) (superNumber: int) =
      WeatherForecastsService.getSuperForecasts logger count superNumber
      |> Task.FromResult

   interface Version1.IWeatherForecastsService with
      member this.GetForecasts count = failwith "Controller should not be called by a marker interface"
      member this.GetSuperForecasts count superNumber = failwith "Controller should not be called by a marker interface"