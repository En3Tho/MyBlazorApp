namespace MyBlazorApp.Services.DiscriminatedUnions.Clients

open System.Net.Http
open System.Net.Http.Json
open System.Runtime.CompilerServices
open System.Text.Json
open System.Threading.Tasks
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging
open MyBlazorApp.Services.DiscriminatedUnions.Contracts.Version1
open MyBlazorApp.Services.DiscriminatedUnions.Contracts
open MyBlazorApp.Utility.Logging
open ILoggerExtensions
open MyBlazorApp.Utility.Http

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

[<Extension; AbstractClass>]
type DependencyInjectionExtensions() =

    [<Extension>]
    static member AddDiscriminatedUnionsHttpClient(services: IServiceCollection, serviceUri) =
        services.AddHttpClient<IDiscriminatedUnionsService, DiscriminatedUnionsServiceVersion1HttpClient>(HttpClient.setBaseAddress serviceUri) |> ignore
        services