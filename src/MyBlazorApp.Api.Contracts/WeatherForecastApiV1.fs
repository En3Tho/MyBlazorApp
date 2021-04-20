module MyBlazorApp.Api.Contracts.WeatherForecastApiV1

open System.Threading.Tasks
open MyBlazorApp.Domain.WeatherForecastsService

let [<Literal>] Version = "v1"

module Routes =
    let [<Literal>] Controller = "weatherforecasts" + "/" + Version + "/"
    let [<Literal>] GetForecasts = "forecasts"
    let [<Literal>] GetSuperForecasts = "superforecasts"

module Endpoints =
    let [<Literal>] GetForecasts = Routes.Controller + Routes.GetForecasts
    let [<Literal>] GetSuperForecasts = Routes.Controller + Routes.GetSuperForecasts

type IService =
    abstract GetForecasts: count: int -> Task<WeatherForecast[]>
    abstract GetSuperForecasts: count: int -> superNumber: int -> Task<WeatherForecast[]>