namespace MyBlazorApp.Services.DiscriminatedUnions.Client.V1

open System
open System.Net.Http
open System.Runtime.CompilerServices
open System.Text.Json
open System.Threading.Tasks
open Microsoft.Extensions.DependencyInjection
open MyBlazorApp.Services.DiscriminatedUnions.Contracts.V1
open En3Tho.FSharp.ComputationExpressions.HttpBuilder

type DiscriminatedUnionsHttpClient(httpClient: HttpClient, jsonSerializerOptions: JsonSerializerOptions) =

    member this.GetRandomImportantData() =
        httpClient
            .Get(Endpoints.GetRandomImportantData)
            .AsJson<ImportantDataDto>(jsonSerializerOptions)
            .Send()

    interface IDiscriminatedUnionsService with
        member this.GetRandomImportantData() = ValueTask<_>(task = this.GetRandomImportantData())

[<Extension; AbstractClass>]
type DependencyInjectionExtensions() =

    [<Extension>]
    static member AddDiscriminatedUnionsHttpClient(services: IServiceCollection) =
        services.AddHttpClient<IDiscriminatedUnionsService, DiscriminatedUnionsHttpClient>(
            fun client -> client.BaseAddress <- Uri Endpoints.ServiceDiscoveryUrl) |> ignore
        services