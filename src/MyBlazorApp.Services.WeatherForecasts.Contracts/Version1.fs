module MyBlazorApp.Services.WeatherForecasts.Contracts.Version1

open System.Threading.Tasks
open MyBlazorApp.Services.WeatherForecasts.Domain

let [<Literal>] Version = "v1"

module Routes =
    let [<Literal>] ServiceName = "weather-forecasts" + "/" + Version + "/"
    let [<Literal>] GetForecasts = "forecasts"
    let [<Literal>] GetSuperForecasts = "super-forecasts"

module Endpoints =
    let [<Literal>] GetForecasts = Routes.ServiceName + Routes.GetForecasts
    let [<Literal>] GetSuperForecasts = Routes.ServiceName + Routes.GetSuperForecasts

type IWeatherForecastsService =
    abstract GetForecasts: count: int -> ValueTask<WeatherForecast[]>
    abstract GetSuperForecasts: count: int -> superNumber: int -> ValueTask<WeatherForecast[]>