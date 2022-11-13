module FSharpComponents.ExampleImports

open En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Core
open En3Tho.FSharp.ComputationExpressions.BlazorBuilder
open Microsoft.AspNetCore.Components

// Auto-generated
// Let this be a class for now, whatever
// let it have special attribute or whatever?
type HelloWorldImport(builder: BlazorBuilderCore) =
    member this.Name2 with set(value: string) =
        builder.AddAttribute("Name2", value)

    member _.Close() = builder.CloseComponent()

type HelloWorld with
    static member inline Render(builder: BlazorBuilderCore, name: string) =
        builder.OpenComponent<HelloWorld>()
        builder.AddAttribute("Name", name)
        HelloWorldImport(builder)

type Importer() =
    inherit ComponentBase()
    override this.BuildRenderTree(builder) =
        builder.Render(blazor {
            fun b -> HelloWorld.Render(b, "C#", Name2 = "VB").Close()
        })