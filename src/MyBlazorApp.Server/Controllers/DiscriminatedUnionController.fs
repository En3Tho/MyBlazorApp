namespace MyBlazorApp.Server.Controllers

open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open MyBlazorApp.Api.Contracts
open MyBlazorApp.Domain

[<Route(DiscriminatedUnionApiV1.Routes.Controller)>]
type DiscriminatedUnionController(logger: ILogger<DiscriminatedUnionController>) =
   inherit ControllerBase()

   [<HttpGet>]
   [<Route(DiscriminatedUnionApiV1.Routes.GetRandomImportantData)>]
   member _.GetRandomImportantData() =
      DiscriminatedUnionService.getRandomImportantData logger
      |> Task.FromResult

   interface DiscriminatedUnionApiV1.IService with
      member this.GetRandomImportantData() = this.GetRandomImportantData()