namespace MyBlazorApp.Services.WeatherForecasts.Server.V1

open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging
open MyBlazorApp.Services.WeatherForecasts.Contracts.V1
open MyBlazorApp.Services.WeatherForecasts.Domain
open En3Tho.FSharp.Extensions.AspNetCore

type WeatherForecastService(logger: ILogger<WeatherForecastService>) =

    member this.GetForecasts(count) =
        WeatherForecastsService.getForecasts logger count
        |> Array.map WeatherForecast.toDto
        |> ValueTask.FromResult

    member this.GetSuperForecasts(count, superNumber) =
        WeatherForecastsService.getSuperForecasts logger count superNumber
        |> Array.map WeatherForecast.toDto
        |> ValueTask.FromResult

    interface IWeatherForecastsService with
        member this.GetForecasts(count) = this.GetForecasts(count)
        member this.GetSuperForecasts(count, superNumber) = this.GetSuperForecasts(count, superNumber)

[<AbstractClass>]
type DependencyInjectionExtensions() = // can be generated automatically from cadl or something?

    [<Extension>]
    static member MapWeatherForecastsService(webApplication: WebApplication) =
        webApplication.MapGet(Endpoints.GetForecasts, (fun (count: int) (service: IWeatherForecastsService) ->
            service.GetForecasts(count)
        )) |> ignore

        webApplication.MapGet(Endpoints.GetSuperForecasts, (fun (count: int) (superNumber: int) (service: IWeatherForecastsService) ->
            service.GetSuperForecasts(count, superNumber)
        )) |> ignore

    [<Extension>]
    static member AddWeatherForecastsService(services: IServiceCollection) =
        services.AddSingleton<IWeatherForecastsService, WeatherForecastService>()