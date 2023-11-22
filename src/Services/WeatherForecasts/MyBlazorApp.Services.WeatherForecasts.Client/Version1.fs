namespace MyBlazorApp.Services.WeatherForecasts.Contracts.V1

open System
open System.Threading.Tasks

module Endpoints =
    let [<Literal>] ServiceName = "weather-forecasts"
    let [<Literal>] ServiceDiscoveryUrl = "http://" + ServiceName
    let [<Literal>] GetForecasts = ServiceName + "/v1/" + "forecasts"
    let [<Literal>] GetSuperForecasts = ServiceName + "/v1/" + "super-forecasts"

type WeatherForecastDto = {
    Date: DateTime
    TemperatureC: int
    Summary: string
}

type IWeatherForecastsServiceV1 =
    abstract GetForecasts: count: int -> ValueTask<WeatherForecastDto[]>
    abstract GetSuperForecasts: count: int * superNumber: int -> ValueTask<WeatherForecastDto[]>