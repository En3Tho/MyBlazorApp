namespace MyBlazorApp.Api.HttpClients

open System.Net.Http
open System.Text.Json
open Microsoft.Extensions.Logging
open MyBlazorApp.Api.Contracts
open MyBlazorApp.Domain.DiscriminatedUnionService
open System.Net.Http.Json
open MyBlazorApp.Utility.Logging.ILoggerExtensions
open FSharp.Control.Tasks.V2

type DiscriminatedUnionApiV1HttpClient(logger: DiscriminatedUnionApiV1HttpClient ILogger,
                                    httpClient: HttpClient,
                                    jsonSerializerOptions: JsonSerializerOptions) =
    member this.GetRandomImportantData() = task {
        let endPoint = DiscriminatedUnionApiV1.Endpoints.GetRandomImportantData

        logger.Tracef $"{nameof this.GetRandomImportantData}. Sending message to: {httpClient.BaseAddress}{endPoint}"
        return! httpClient.GetFromJsonAsync<ImportantData>(endPoint, jsonSerializerOptions).ConfigureAwait false
    }

    interface DiscriminatedUnionApiV1.IService with
        member this.GetRandomImportantData() = this.GetRandomImportantData()