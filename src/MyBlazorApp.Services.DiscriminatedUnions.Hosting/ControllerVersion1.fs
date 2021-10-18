namespace MyBlazorApp.Services.DiscriminatedUnions.Hosting

open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open MyBlazorApp.Services.DiscriminatedUnions.Contracts.Version1
open MyBlazorApp.Services.DiscriminatedUnions.CrossCutting
open MyBlazorApp.Services.DiscriminatedUnions.Domain

type DiscriminatedUnionsServiceVersion1(logger: DiscriminatedUnionsServiceVersion1 ILogger) =

    member this.GetRandomImportantData() =
        DiscriminatedUnions.getRandomImportantData logger
        |> ImportantData.toDto
        |> ValueTask.FromResult

    interface IDiscriminatedUnionsService with
        member this.GetRandomImportantData() = this.GetRandomImportantData()

[<Route(Routes.Controller)>]
type DiscriminatedUnionsController(service: IDiscriminatedUnionsService, logger: ILogger<DiscriminatedUnionsController>) =
   inherit ControllerBase()

   [<HttpGet(Routes.GetRandomImportantData)>]
   member _.GetRandomImportantData() =
      service.GetRandomImportantData()