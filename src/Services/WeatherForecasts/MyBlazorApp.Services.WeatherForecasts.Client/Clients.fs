namespace MyBlazorApp.Services.WeatherForecasts.Clients

open System
open System.Net.Http
open System.Runtime.CompilerServices
open System.Text.Json
open System.Threading.Tasks
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging
open Microsoft.Extensions.Options
open MyBlazorApp.Services.WeatherForecasts.Client.Version1
open System.Net.Http.Json
open MyBlazorApp.Utility.Logging.ILoggerExtensions
open MyBlazorApp.Utility.Http

[<CLIMutable>]
type WeatherForecastsServiceConnectionSettings = {
    Uri: string
}

[<Sealed>]
type WeatherForecastsApiVersion1HttpClient(logger: ILogger<WeatherForecastsApiVersion1HttpClient>,
                                           settings: IOptions<WeatherForecastsServiceConnectionSettings>,
                                           httpClient: HttpClient,
                                           jsonSerializerOptions: JsonSerializerOptions) =

    do httpClient.BaseAddress <- Uri settings.Value.Uri

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

    interface IWeatherForecastsServiceV1 with
       member this.GetForecasts count = ValueTask<_>(task = this.GetForecasts count)
       member this.GetSuperForecasts (count, superNumber) = ValueTask<_>(task = this.GetSuperForecasts count superNumber)

[<Extension; AbstractClass>]
type DependencyInjectionExtensions() =

    [<Extension>]
    static member AddWeatherForecastsHttpClient(services: IServiceCollection, configuration: IConfiguration) =
        services.Configure<WeatherForecastsServiceConnectionSettings>(configuration.GetSection(nameof(WeatherForecastsServiceConnectionSettings)))
                .AddHttpClient<IWeatherForecastsServiceV1, WeatherForecastsApiVersion1HttpClient>() |> ignore
        services