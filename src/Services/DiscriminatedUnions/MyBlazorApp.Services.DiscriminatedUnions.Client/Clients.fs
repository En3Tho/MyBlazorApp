namespace MyBlazorApp.Services.DiscriminatedUnions.Client.V1

open System
open System.Net.Http
open System.Runtime.CompilerServices
open System.Text.Json
open System.Threading.Tasks
open Microsoft.Extensions.DependencyInjection
open MyBlazorApp.Services.DiscriminatedUnions.Contracts.V1
open En3Tho.FSharp.ComputationExpressions.HttpBuilder

type DiscriminatedUnionsServiceV1HttpClient(httpClient: HttpClient,
                                            jsonSerializerOptions: JsonSerializerOptions) =

    member this.GetRandomImportantData() =
        httpClient
            .Get(Endpoints.GetRandomImportantData)
            .AsJson<ImportantDataDto>(jsonSerializerOptions)
            .SendRequest()

    interface IDiscriminatedUnionsServiceV1 with
        member this.GetRandomImportantData() = ValueTask<_>(task = this.GetRandomImportantData())

[<Extension; AbstractClass>]
type DependencyInjectionExtensions() =

    [<Extension>]
    static member AddDiscriminatedUnionsHttpClient(services: IServiceCollection) =
        services.AddHttpClient<IDiscriminatedUnionsServiceV1, DiscriminatedUnionsServiceV1HttpClient>(
            fun client -> client.BaseAddress <- Uri Endpoints.ServiceDiscoveryUrl) |> ignore
        services