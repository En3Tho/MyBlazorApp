namespace MyBlazorApp.Services.WeatherForecasts.Hosting.DependencyInjection

open System.Runtime.CompilerServices
open Microsoft.Extensions.DependencyInjection
open MyBlazorApp.Services.WeatherForecasts.Controller

[<Extension; AbstractClass>]
type DependencyInjectionExtensions() =
    [<Extension>]
    static member AddWeatherForecastsController(builder: IMvcBuilder) =
        builder.AddApplicationPart(typeof<WeatherForecastsServiceVersion1Controller>.Assembly)