namespace MyBlazorApp.Server.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open MyBlazorApp.Api.Contracts
open MyBlazorApp.Domain

[<Route(WeatherForecastApiV1.Routes.Controller)>]
type WeatherForecastsController(logger: ILogger<WeatherForecastsController>) =
   inherit ControllerBase()

   [<HttpGet>]
   [<Route(WeatherForecastApiV1.Routes.GetForecasts)>]
   member _.GetForecasts count =
      WeatherForecastsService.getForecasts logger count

   [<HttpGet>]
   [<Route(WeatherForecastApiV1.Routes.GetSuperForecasts)>]
   member _.GetSuperForecasts count (superNumber: int) =
      WeatherForecastsService.getSuperForecasts logger count superNumber

   interface WeatherForecastApiV1.IContract with
      member this.GetForecasts count = this.GetForecasts count
      member this.GetSuperForecasts count superNumber = this.GetSuperForecasts count superNumber