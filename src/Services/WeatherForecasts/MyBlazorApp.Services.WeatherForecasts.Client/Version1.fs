module MyBlazorApp.Services.WeatherForecasts.Client.Version1

open System
open System.Threading.Tasks

let [<Literal>] Version = "v1"

module Routes =
    let [<Literal>] ServiceName = "weather-forecasts" + "/" + Version + "/"
    let [<Literal>] GetForecasts = "forecasts"
    let [<Literal>] GetSuperForecasts = "super-forecasts"

module Endpoints =
    let [<Literal>] GetForecasts = Routes.ServiceName + Routes.GetForecasts
    let [<Literal>] GetSuperForecasts = Routes.ServiceName + Routes.GetSuperForecasts

type WeatherForecastDto = {
    Date: DateTime
    TemperatureC: int
    Summary: string
}

type IWeatherForecastsServiceV1 =
    abstract GetForecasts: count: int -> ValueTask<WeatherForecastDto[]>
    abstract GetSuperForecasts: count: int * superNumber: int -> ValueTask<WeatherForecastDto[]>