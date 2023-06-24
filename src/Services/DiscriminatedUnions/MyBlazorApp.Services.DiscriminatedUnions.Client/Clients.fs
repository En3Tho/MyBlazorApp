namespace MyBlazorApp.Services.DiscriminatedUnions.Clients

open System
open System.Net.Http
open System.Net.Http.Json
open System.Runtime.CompilerServices
open System.Text.Json
open System.Threading.Tasks
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging
open Microsoft.Extensions.Options
open MyBlazorApp.Services.DiscriminatedUnions.Client
open MyBlazorApp.Utility.Logging
open ILoggerExtensions

[<CLIMutable>]
type DiscriminatedUnionsServiceConnectionSettings = {
    Uri: string
}

type DiscriminatedUnionsServiceV1HttpClient(logger: ILogger<DiscriminatedUnionsServiceV1HttpClient>,
                                            settings: IOptions<DiscriminatedUnionsServiceConnectionSettings>,
                                            httpClient: HttpClient,
                                            jsonSerializerOptions: JsonSerializerOptions) =

    // TODO this vs IOptions?
    do httpClient.BaseAddress <- Uri settings.Value.Uri

    member this.GetRandomImportantData() = task {
        let endPoint = Endpoints.GetRandomImportantData
        logger.Tracef $"{nameof this.GetRandomImportantData}. Sending message to: {httpClient.BaseAddress}{endPoint}"
        return! httpClient.GetFromJsonAsync<ImportantDataDto>(endPoint, jsonSerializerOptions).ConfigureAwait false
    }

    interface IDiscriminatedUnionsServiceV1 with
        member this.GetRandomImportantData() = ValueTask<_>(task = this.GetRandomImportantData())

[<Extension; AbstractClass>]
type DependencyInjectionExtensions() =

    [<Extension>]
    static member AddDiscriminatedUnionsHttpClient(services: IServiceCollection, configuration: IConfiguration) =
        services.Configure<DiscriminatedUnionsServiceConnectionSettings>(configuration.GetSection(nameof(DiscriminatedUnionsServiceConnectionSettings)))
                .AddHttpClient<IDiscriminatedUnionsServiceV1, DiscriminatedUnionsServiceV1HttpClient>() |> ignore
        services