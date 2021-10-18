namespace MyBlazorApp.Services.WeatherForecasts.Hosting.DependencyInjection

open System.Runtime.CompilerServices
open Microsoft.Extensions.DependencyInjection
open MyBlazorApp.Services.WeatherForecasts.Contracts.Version1
open MyBlazorApp.Services.WeatherForecasts.Controller

[<Extension; AbstractClass>]
type DependencyInjectionExtensions() =

    [<Extension>]
    static member AddWeatherForecastsService(services: IServiceCollection) =
        services.AddMvcCore().AddApplicationPart(typeof<WeatherForecastsServiceVersion1Controller>.Assembly)|> ignore
        services.AddSingleton<IWeatherForecastsService, WeatherForecastServiceVersion1>()