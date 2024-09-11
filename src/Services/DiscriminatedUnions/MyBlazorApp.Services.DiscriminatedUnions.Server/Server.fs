namespace MyBlazorApp.Services.DiscriminatedUnions.Server.V1

open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging
open MyBlazorApp.Services.DiscriminatedUnions.Contracts.V1
open MyBlazorApp.Services.DiscriminatedUnions.Domain
open Microsoft.AspNetCore.Builder
open En3Tho.FSharp.Extensions.AspNetCore

type DiscriminatedUnionsService(logger: ILogger<DiscriminatedUnionsService>) =

    member this.GetRandomImportantData() =
        DiscriminatedUnions.getRandomImportantData logger
        |> ImportantData.toDto
        |> ValueTask.FromResult

    interface IDiscriminatedUnionsService with
        member this.GetRandomImportantData() = this.GetRandomImportantData()

[<AbstractClass>]
type DependencyInjectionExtensions() =

    [<Extension>]
    static member MapDiscriminatedUnionsService(webApplication: WebApplication) =
        webApplication.MapGet(Endpoints.GetRandomImportantData, (fun (service: IDiscriminatedUnionsService) ->
            service.GetRandomImportantData()
        )) |> ignore

    [<Extension>]
    static member AddDiscriminatedUnionsService(services: IServiceCollection) =
        services.AddSingleton<IDiscriminatedUnionsService, DiscriminatedUnionsService>()