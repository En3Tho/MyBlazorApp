namespace MyBlazorApp.Services.DiscriminatedUnions.Hosting

open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open MyBlazorApp.Services.DiscriminatedUnions.Contracts.Version1
open MyBlazorApp.Services.DiscriminatedUnions.Domain

[<Route(Routes.Controller)>]
type DiscriminatedUnionController(logger: ILogger<DiscriminatedUnionController>) =
   inherit ControllerBase()

   [<HttpGet(Routes.GetRandomImportantData)>]
   member _.GetRandomImportantData() =
      DiscriminatedUnionsService.getRandomImportantData logger
      |> ValueTask.FromResult

   interface IDiscriminatedUnionsService with
      member this.GetRandomImportantData() = failwith "Controller should not be called by a marker interface"