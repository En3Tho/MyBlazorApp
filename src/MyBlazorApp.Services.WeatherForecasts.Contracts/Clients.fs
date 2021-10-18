namespace MyBlazorApp.Services.WeatherForecasts.Clients

open System.Net.Http
open System.Text.Json
open System.Threading.Tasks
open Microsoft.Extensions.Logging
open MyBlazorApp.Services.WeatherForecasts.Contracts.Version1
open System.Net.Http.Json
open MyBlazorApp.Utility.Logging.ILoggerExtensions
open MyBlazorApp.Utility.Http

[<CLIMutable>]
type WeatherForecastsServiceConnectionSettings = {
    Uri: string
}

type WeatherForecastsApiVersion1HttpClient(logger: WeatherForecastsApiVersion1HttpClient ILogger,
                                           httpClient: HttpClient,
                                           jsonSerializerOptions: JsonSerializerOptions) =

    member this.GetForecasts (count: int) = task {
        let endPoint =
            UriHelper.GetParametrizedUriString(
                Endpoints.GetForecasts,
                nameof count, count)

        logger.Tracef $"{nameof this.GetForecasts}. Sending message to: {httpClient.BaseAddress}{endPoint}"
        return! httpClient.GetFromJsonAsync<WeatherForecastDto[]>(endPoint, jsonSerializerOptions).ConfigureAwait false
    }

    member this.GetSuperForecasts (count: int) (superNumber: int) = task {
        let endPoint =
            UriHelper.GetParametrizedUriString(
                Endpoints.GetSuperForecasts,
                nameof count, count,
                nameof superNumber, superNumber)

        logger.Tracef $"{nameof this.GetSuperForecasts}. Sending message to: {httpClient.BaseAddress}{endPoint}"
        return! httpClient.GetFromJsonAsync<WeatherForecastDto[]>(endPoint, jsonSerializerOptions).ConfigureAwait false
    }

    interface IWeatherForecastsService with
       member this.GetForecasts count = ValueTask<_>(task = this.GetForecasts count)
       member this.GetSuperForecasts (count, superNumber) = ValueTask<_>(task = this.GetSuperForecasts count superNumber)