namespace MyBlazorApp.Services.WeatherForecasts.Client.V1

open System
open System.Net.Http
open System.Runtime.CompilerServices
open System.Text.Json
open System.Threading.Tasks
open Microsoft.Extensions.DependencyInjection
open En3Tho.FSharp.ComputationExpressions.HttpBuilder
open MyBlazorApp.Services.WeatherForecasts.Contracts.V1
open MyBlazorApp.Utility.Http

[<Sealed>]
type WeatherForecastsHttpClient(httpClient: HttpClient, jsonSerializerOptions: JsonSerializerOptions) =

    member this.GetForecasts(count: int) =
        let endPoint =
            UriHelper.GetParametrizedUriString(
                Endpoints.GetForecasts,
                nameof count, count)

        httpClient
            .Get(endPoint)
            .AsJson<WeatherForecastDto[]>(jsonSerializerOptions)
            .Send()

    member this.GetSuperForecasts(count: int, superNumber: int) =
        let endPoint =
            UriHelper.GetParametrizedUriString(
                Endpoints.GetSuperForecasts,
                nameof count, count,
                nameof superNumber, superNumber)

        httpClient
            .Get(endPoint)
            .AsJson<WeatherForecastDto[]>(jsonSerializerOptions)
            .Send()

    interface IWeatherForecastsService with
       member this.GetForecasts count = ValueTask<_>(task = this.GetForecasts(count))
       member this.GetSuperForecasts (count, superNumber) = ValueTask<_>(task = this.GetSuperForecasts(count, superNumber))

[<Extension; AbstractClass>]
type DependencyInjectionExtensions() =

    [<Extension>]
    static member AddWeatherForecastsHttpClient(services: IServiceCollection) =
        services.AddHttpClient<IWeatherForecastsService, WeatherForecastsHttpClient>(
            fun client -> client.BaseAddress <- Uri Endpoints.ServiceDiscoveryUrl) |> ignore
        services