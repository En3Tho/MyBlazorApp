namespace MyBlazorApp.Services.WeatherForecasts.Clients

open System
open System.Net.Http
open System.Text.Json
open System.Threading.Tasks
open Microsoft.Extensions.Logging
open Microsoft.Extensions.Options
open MyBlazorApp.Services.WeatherForecasts.Contracts
open MyBlazorApp.Services.WeatherForecasts.Domain
open System.Net.Http.Json
open MyBlazorApp.Utility.Logging.ILoggerExtensions
open FSharp.Control.Tasks.V2
open MyBlazorApp.Utility.Http

[<CLIMutable>]
type WeatherForecastsServiceConnectionSettings = {
    Uri: string
}

type WeatherForecastsApiVersion1HttpClient(logger: WeatherForecastsApiVersion1HttpClient ILogger,
                                           httpClient: HttpClient,
                                           jsonSerializerOptions: JsonSerializerOptions,
                                           connectionSettings: WeatherForecastsServiceConnectionSettings IOptions) =

    let settings = connectionSettings.Value
    do httpClient.BaseAddress <- Uri settings.Uri

    member this.GetForecasts (count: int) = task {
        let endPoint =
            UriHelper.GetParametrizedUriString(
                Version1.Endpoints.GetForecasts,
                nameof count, count)


        logger.Tracef $"{nameof this.GetForecasts}. Sending message to: {settings.Uri}{endPoint}"
        return! httpClient.GetFromJsonAsync<WeatherForecast[]>(endPoint, jsonSerializerOptions).ConfigureAwait false
    }

    member this.GetSuperForecasts (count: int) (superNumber: int) = task {
        let endPoint =
            UriHelper.GetParametrizedUriString(
                Version1.Endpoints.GetSuperForecasts,
                nameof count, count,
                nameof superNumber, superNumber)

        logger.Tracef $"{nameof this.GetSuperForecasts}. Sending message to: {settings.Uri}{endPoint}"
        return! httpClient.GetFromJsonAsync<WeatherForecast[]>(endPoint, jsonSerializerOptions).ConfigureAwait false
    }

    interface Version1.IWeatherForecastsService with
       member this.GetForecasts count = ValueTask<_>(task = this.GetForecasts count)
       member this.GetSuperForecasts count superNumber = ValueTask<_>(task = this.GetSuperForecasts count superNumber)

type WeatherForecastServiceVersion1(logger: WeatherForecastServiceVersion1 ILogger) =

    member this.GetForecasts count =
        WeatherForecastsService.getForecasts logger count |> ValueTask.FromResult

    member this.GetSuperForecasts count superNumber =
        WeatherForecastsService.getSuperForecasts logger count superNumber |> ValueTask.FromResult

    interface Version1.IWeatherForecastsService with
        member this.GetForecasts count = this.GetForecasts count
        member this.GetSuperForecasts count superNumber = this.GetSuperForecasts count superNumber