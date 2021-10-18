namespace MyBlazorApp.Services.DiscriminatedUnions.DependencyInjection

open System.Runtime.CompilerServices
open Microsoft.Extensions.DependencyInjection
open MyBlazorApp.Services.DiscriminatedUnions.Contracts.Version1
open MyBlazorApp.Services.DiscriminatedUnions.Hosting

[<Extension; AbstractClass>]
type DependencyInjectionExtensions() =

    [<Extension>]
    static member AddDiscriminatedUnionsService(services: IServiceCollection) =
        services.AddMvcCore().AddApplicationPart(typeof<DiscriminatedUnionsController>.Assembly) |> ignore
        services.AddSingleton<IDiscriminatedUnionsService, DiscriminatedUnionsServiceVersion1>()