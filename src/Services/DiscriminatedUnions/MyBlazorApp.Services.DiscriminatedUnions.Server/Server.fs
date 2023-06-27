namespace MyBlazorApp.Services.DiscriminatedUnions.Server

open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging
open MyBlazorApp.Services.DiscriminatedUnions.Client
open MyBlazorApp.Services.DiscriminatedUnions.Domain
open MyBlazorApp.Services.DiscriminatedUnions.Server
open Microsoft.AspNetCore.Builder
open En3Tho.FSharp.Extensions.AspNetCore

type DiscriminatedUnionsServiceV1(logger: ILogger<DiscriminatedUnionsServiceV1>) =

    member this.GetRandomImportantData() =
        DiscriminatedUnions.getRandomImportantData logger
        |> ImportantData.toDto
        |> ValueTask.FromResult

    interface IDiscriminatedUnionsServiceV1 with
        member this.GetRandomImportantData() = this.GetRandomImportantData()

[<Extension; AbstractClass>]
type DependencyInjectionExtensions() =

    [<Extension>]
    static member MapDiscriminatedUnionsEndpoints(webApplication: WebApplication) =
        webApplication.MapGet(Endpoints.GetRandomImportantData, (fun (service: IDiscriminatedUnionsServiceV1) ->
            service.GetRandomImportantData()
        )) |> ignore

    [<Extension>]
    static member AddDiscriminatedUnionsService(services: IServiceCollection) =
        services.AddSingleton<IDiscriminatedUnionsServiceV1, DiscriminatedUnionsServiceV1>()