namespace MyBlazorApp.Services.WeatherForecasts.Server.V1

open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging
open MyBlazorApp.Services.WeatherForecasts.Contracts.V1
open MyBlazorApp.Services.WeatherForecasts.Domain
open En3Tho.FSharp.Extensions.AspNetCore

type WeatherForecastServiceV1(logger: ILogger<WeatherForecastServiceV1>) =

    member this.GetForecasts(count) =
        WeatherForecastsService.getForecasts logger count
        |> Array.map WeatherForecast.toDto
        |> ValueTask.FromResult

    member this.GetSuperForecasts(count, superNumber) =
        WeatherForecastsService.getSuperForecasts logger count superNumber
        |> Array.map WeatherForecast.toDto
        |> ValueTask.FromResult

    interface IWeatherForecastsServiceV1 with
        member this.GetForecasts count = this.GetForecasts(count)
        member this.GetSuperForecasts (count, superNumber) = this.GetSuperForecasts(count, superNumber)

// TODO: better in C#?
[<Extension; AbstractClass>]
type DependencyInjectionExtensions() = // can be generated automatically from cadl or something?

    [<Extension>]
    static member MapWeatherForecastsServiceEndpoints(webApplication: WebApplication) =
        webApplication.MapGet(Endpoints.GetForecasts, (fun (count: int) (service: IWeatherForecastsServiceV1) ->
            service.GetForecasts(count)
        )) |> ignore

        webApplication.MapGet(Endpoints.GetSuperForecasts, (fun (count: int) (superNumber: int) (service: IWeatherForecastsServiceV1) ->
            service.GetSuperForecasts(count, superNumber)
        )) |> ignore

    [<Extension>]
    static member AddWeatherForecastsService(services: IServiceCollection) =
        services.AddSingleton<IWeatherForecastsServiceV1, WeatherForecastServiceV1>()