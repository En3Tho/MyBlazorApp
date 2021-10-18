namespace MyBlazorApp.Services.DiscriminatedUnions.Clients

open System.Net.Http
open System.Net.Http.Json
open System.Text.Json
open System.Threading.Tasks
open FSharp.Control.Tasks
open Microsoft.Extensions.Logging
open MyBlazorApp.Services.DiscriminatedUnions.Contracts.Version1
open MyBlazorApp.Services.DiscriminatedUnions.Contracts
open MyBlazorApp.Utility.Logging
open ILoggerExtensions

[<CLIMutable>]
type DiscriminatedUnionsServiceConnectionSettings = {
    Uri: string
}

type DiscriminatedUnionsServiceVersion1HttpClient(logger: DiscriminatedUnionsServiceVersion1HttpClient ILogger,
                                                  httpClient: HttpClient,
                                                  jsonSerializerOptions: JsonSerializerOptions) =

    member this.GetRandomImportantData() = task {
        let endPoint = Endpoints.GetRandomImportantData

        logger.Tracef $"{nameof this.GetRandomImportantData}. Sending message to: {httpClient.BaseAddress}{endPoint}"
        return! httpClient.GetFromJsonAsync<ImportantDataDto>(endPoint, jsonSerializerOptions).ConfigureAwait false
    }

    interface Version1.IDiscriminatedUnionsService with
        member this.GetRandomImportantData() = ValueTask<_>(task = this.GetRandomImportantData())