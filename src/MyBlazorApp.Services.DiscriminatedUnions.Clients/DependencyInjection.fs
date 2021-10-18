namespace MyBlazorApp.Services.DiscriminatedUnions.DependencyInjection

open System.Runtime.CompilerServices
open Microsoft.Extensions.DependencyInjection
open MyBlazorApp.Services.DiscriminatedUnions.Clients
open MyBlazorApp.Services.DiscriminatedUnions.Contracts.Version1
open MyBlazorApp.Utility.Http

[<Extension; AbstractClass>]
type DependencyInjectionExtensions() =

    [<Extension>]
    static member AddDiscriminatedUnionsHttpClient(services: IServiceCollection, serviceUri) =
        services.AddHttpClient<IDiscriminatedUnionsService, DiscriminatedUnionsServiceVersion1HttpClient>(HttpClient.setBaseAddress serviceUri) |> ignore
        services