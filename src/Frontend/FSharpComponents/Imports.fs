module FSharpComponents.Imports

open System
open System.Runtime.CompilerServices
open En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Core
open Microsoft.AspNetCore.Components
open TailwindComponents.Basics
open TailwindComponents.CodinGame

// Ideally this should be Auto-generated
type [<Struct; IsReadOnly>] MatrixImport(builder: BlazorBuilderCore) =

    interface IComponentImport with
        member _.Builder = builder

type Matrix with
    static member inline Render(builder: BlazorBuilderCore, data: int[][]) =
        builder.OpenComponent<Matrix>()
        builder.AddAttribute("Data", data)
        MatrixImport(builder)

type [<Struct; IsReadOnly>] HelloWorldImport(builder: BlazorBuilderCore) =

    member this.Name2 with set(value: string) =
        builder.AddAttribute("Name2", value)

    interface IComponentImport with
        member _.Builder = builder

type HelloWorld with
    static member inline Render(builder: BlazorBuilderCore, name: string) =
        builder.OpenComponent<HelloWorld>()
        builder.AddAttribute("Name", name)
        HelloWorldImport(builder)

type [<Struct; IsReadOnly>] RequiredImport(builder: BlazorBuilderCore) =

    interface IComponentImport with
        member _.Builder = builder

type Required with
    static member inline Render(builder: BlazorBuilderCore, error: RenderFragment, main: RenderFragment, validator: Func<bool>) =
        builder.OpenComponent<Required>()
        builder.AddAttribute("Error", error)
        builder.AddAttribute("Main", main)
        builder.AddAttribute("Validator", validator)
        RequiredImport(builder)