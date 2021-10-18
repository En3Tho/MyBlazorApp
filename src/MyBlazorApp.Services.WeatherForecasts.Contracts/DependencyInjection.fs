namespace MyBlazorApp.Services.WeatherForecasts.Clients.DependencyInjection

open System.Runtime.CompilerServices
open Microsoft.Extensions.DependencyInjection
open MyBlazorApp.Services.WeatherForecasts.Clients
open MyBlazorApp.Services.WeatherForecasts.Contracts.Version1
open MyBlazorApp.Utility.Http

[<Extension; AbstractClass>]
type DependencyInjectionExtensions() =

    [<Extension>]
    static member AddWeatherForecastsHttpClient(services: IServiceCollection, serviceUri) =
        services.AddHttpClient<IWeatherForecastsService, WeatherForecastsApiVersion1HttpClient>(HttpClient.setBaseAddress serviceUri) |> ignore
        services