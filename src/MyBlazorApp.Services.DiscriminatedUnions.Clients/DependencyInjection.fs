namespace MyBlazorApp.Services.DiscriminatedUnions.DependencyInjection

open System.Runtime.CompilerServices
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Options
open MyBlazorApp.Services.DiscriminatedUnions.Clients
open MyBlazorApp.Services.DiscriminatedUnions.Contracts.Version1

[<Extension; AbstractClass>]
type DependencyInjectionExtensions() =
    [<Extension>]
    static member AddDiscriminatedUnionsService(services: IServiceCollection) =
        services.AddSingleton<IDiscriminatedUnionsService, DiscriminatedUnionsServiceVersion1>()

    [<Extension>]
    static member AddDiscriminatedUnionsHttpClient(services: IServiceCollection) =
        services.AddHttpClient<IDiscriminatedUnionsService, DiscriminatedUnionsServiceVersion1HttpClient>() |> ignore
        services