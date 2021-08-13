namespace MyBlazorApp.Services.DiscriminatedUnions.Clients

open System
open System.Net.Http
open System.Net.Http.Json
open System.Text.Json
open System.Threading.Tasks
open FSharp.Control.Tasks
open Microsoft.Extensions.Logging
open Microsoft.Extensions.Options
open MyBlazorApp.Services.DiscriminatedUnions.Domain
open MyBlazorApp.Services.DiscriminatedUnions.Contracts
open MyBlazorApp.Utility.Logging
open ILoggerExtensions

[<CLIMutable>]
type DiscriminatedUnionsServiceConnectionSettings = {
    Uri: string
}

type DiscriminatedUnionsServiceVersion1HttpClient(logger: DiscriminatedUnionsServiceVersion1HttpClient ILogger,
                                                  httpClient: HttpClient,
                                                  jsonSerializerOptions: JsonSerializerOptions,
                                                  connectionSettings: DiscriminatedUnionsServiceConnectionSettings IOptions) =

    let settings = connectionSettings.Value
    do httpClient.BaseAddress <- Uri settings.Uri

    member this.GetRandomImportantData() = task {
        let endPoint = Version1.Endpoints.GetRandomImportantData

        logger.Tracef $"{nameof this.GetRandomImportantData}. Sending message to: {settings.Uri}{endPoint}"
        return! httpClient.GetFromJsonAsync<ImportantData>(endPoint, jsonSerializerOptions).ConfigureAwait false
    }

    interface Version1.IDiscriminatedUnionsService with
        member this.GetRandomImportantData() = ValueTask<_>(task = this.GetRandomImportantData())

type DiscriminatedUnionsServiceVersion1(logger: DiscriminatedUnionsServiceVersion1 ILogger) =

    member this.GetRandomImportantData() =
        DiscriminatedUnionsService.getRandomImportantData logger |> ValueTask.FromResult

    interface Version1.IDiscriminatedUnionsService with
        member this.GetRandomImportantData() = this.GetRandomImportantData()