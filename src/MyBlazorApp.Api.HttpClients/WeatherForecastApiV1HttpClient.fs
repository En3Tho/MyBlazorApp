namespace MyBlazorApp.Api.HttpClients

open System.Net.Http
open System.Text.Json
open Microsoft.Extensions.Logging
open MyBlazorApp.Api.Contracts
open MyBlazorApp.Domain.WeatherForecastsService
open System.Net.Http.Json
open MyBlazorApp.Utility.Logging.ILoggerExtensions
open FSharp.Control.Tasks.V2
open MyBlazorApp.Utility.Http

type WeatherForecastApiV1HttpClient(logger: WeatherForecastApiV1HttpClient ILogger,
                                    httpClient: HttpClient,
                                    jsonSerializerOptions: JsonSerializerOptions) =
    member this.GetForecasts (count: int) = task {
        let endPoint =
            UriHelper.GetParametrizedUriString(
                WeatherForecastApiV1.Endpoints.GetForecasts,
                nameof count, count)

        logger.Tracef $"{nameof this.GetForecasts}. Sending message to: {httpClient.BaseAddress}{endPoint}"
        return! httpClient.GetFromJsonAsync<WeatherForecast[]>(endPoint, jsonSerializerOptions).ConfigureAwait false
    }

    member this.GetSuperForecasts (count: int) (superNumber: int) = task {
        let endPoint =
            UriHelper.GetParametrizedUriString(
                WeatherForecastApiV1.Endpoints.GetSuperForecasts,
                nameof count, count,
                nameof superNumber, superNumber)

        logger.Tracef $"{nameof this.GetSuperForecasts}. Sending message to: {httpClient.BaseAddress}{endPoint}"
        return! httpClient.GetFromJsonAsync<WeatherForecast[]>(endPoint, jsonSerializerOptions).ConfigureAwait false
    }

    interface WeatherForecastApiV1.IService with
       member this.GetForecasts count = this.GetForecasts count
       member this.GetSuperForecasts count superNumber = this.GetSuperForecasts count superNumber