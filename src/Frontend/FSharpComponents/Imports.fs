module FSharpComponents.Imports

open System
open System.Runtime.CompilerServices
open En3Tho.FSharp.BlazorBuilder.Core
open Microsoft.AspNetCore.Components
open TailwindComponents.Basics
open TailwindComponents.CodinGame

// namespace TailwindComponents.CodinGame

// module ImportStubs =
    // open ... ?
    // type [<Struct; IsReadOnly>] MatrixImport(builder: BlazorBuilderCore) =
    //
    // interface IComponentImport with
    //     member _.Builder = builder

    // type Matrix with
    //     static member inline Render(builder: BlazorBuilderCore, data: int[][]) =
    //         builder.OpenComponent<Matrix>()
    //         builder.AddAttribute("Data", data)
    //         MatrixImport(builder)

// [<AbstractClass; Sealed; AutoOpen>]
// type Imports() =
    //static member inline Matrix'(builder, data) = Matrix.Render(builder, data)

// Ideally this should be Auto-generated
type [<Struct; IsReadOnly>] MatrixImport(builder: BlazorBuilderCore) =

    interface IComponentImport with
        member _.Builder = builder

type Matrix with
    static member inline Render(builder: BlazorBuilderCore, data: int[][]) =
        builder.OpenComponent<Matrix>()
        builder.AddAttribute("Data", data)
        MatrixImport(builder)

type [<Struct; IsReadOnly>] HelloWorldFSharpImport(builder: BlazorBuilderCore) =

    member this.Name2 with set(value: string) =
        builder.AddAttribute("Name2", value)

    interface IComponentImport with
        member _.Builder = builder

type HelloWorldFSharp with
    static member inline Render(builder: BlazorBuilderCore, name: string) =
        builder.OpenComponent<HelloWorldFSharp>()
        builder.AddAttribute("Name", name)
        HelloWorldFSharpImport(builder)

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