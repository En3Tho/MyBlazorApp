namespace MyBlazorApp.Services.WeatherForecasts.Clients.DependencyInjection

open System.Runtime.CompilerServices
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Options
open MyBlazorApp.Services.WeatherForecasts.Clients
open MyBlazorApp.Services.WeatherForecasts.Contracts.Version1

[<Extension; AbstractClass>]
type DependencyInjectionExtensions() =
    [<Extension>]
    static member AddWeatherForecastsService(services: IServiceCollection) =
        services.AddSingleton<IWeatherForecastsService, WeatherForecastServiceVersion1>()

    [<Extension>]
    static member AddWeatherForecastsHttpClient(services: IServiceCollection) =
        services.AddHttpClient<IWeatherForecastsService, WeatherForecastsApiVersion1HttpClient>() |> ignore
        services