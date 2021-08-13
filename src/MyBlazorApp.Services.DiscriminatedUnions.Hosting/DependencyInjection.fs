namespace MyBlazorApp.Services.DiscriminatedUnions.DependencyInjection

open System.Runtime.CompilerServices
open Microsoft.Extensions.DependencyInjection
open MyBlazorApp.Services.DiscriminatedUnions.Hosting

[<Extension; AbstractClass>]
type DependencyInjectionExtensions() =
    [<Extension>]
    static member AddDiscriminatedUnionsController(builder: IMvcBuilder) =
        builder.AddApplicationPart(typeof<DiscriminatedUnionController>.Assembly)