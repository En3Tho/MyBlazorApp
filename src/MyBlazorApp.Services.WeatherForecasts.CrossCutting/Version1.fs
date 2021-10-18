﻿namespace MyBlazorApp.Services.WeatherForecasts.CrossCutting

open MyBlazorApp.Services.WeatherForecasts.Contracts.Version1
open MyBlazorApp.Services.WeatherForecasts.Domain

module WeatherForecast =
    let toDto (forecast: WeatherForecast) : WeatherForecastDto = {
       Date = forecast.Date
       TemperatureC = forecast.TemperatureC
       Summary = forecast.Summary
    }

    let fromDto (forecast: WeatherForecastDto) : WeatherForecast = {
        Date = forecast.Date
        TemperatureC = forecast.TemperatureC
        Summary = forecast.Summary
    }