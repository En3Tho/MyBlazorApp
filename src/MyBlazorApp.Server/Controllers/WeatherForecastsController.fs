namespace MyBlazorApp.Server.Controllers

open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open MyBlazorApp.Api.Contracts
open MyBlazorApp.Domain

[<Route(WeatherForecastApiV1.Routes.Controller)>]
type WeatherForecastsController(logger: ILogger<WeatherForecastsController>) =
   inherit ControllerBase()

   [<HttpGet>]
   [<Route(WeatherForecastApiV1.Routes.GetForecasts)>]
   member _.GetForecasts (count: int) =
      WeatherForecastsService.getForecasts logger count
      |> Task.FromResult

   [<HttpGet>]
   [<Route(WeatherForecastApiV1.Routes.GetSuperForecasts)>]
   member _.GetSuperForecasts (count: int) (superNumber: int) =
      WeatherForecastsService.getSuperForecasts logger count superNumber
      |> Task.FromResult

   interface WeatherForecastApiV1.IService with
      member this.GetForecasts count = this.GetForecasts count
      member this.GetSuperForecasts count superNumber = this.GetSuperForecasts count superNumber