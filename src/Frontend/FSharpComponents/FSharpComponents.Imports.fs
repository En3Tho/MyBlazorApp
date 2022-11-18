// Auto-generated
namespace FSharpComponents
open System.Runtime.CompilerServices
open En3Tho.FSharp.BlazorBuilder.Core

[<AutoOpen>]
module HelloWorldFSharp__Import =
    open FSharpComponents
    open System

    type [<Struct; IsReadOnly>] HelloWorldFSharp__Import(builder: BlazorBuilderCore) =

        member this.Name2 with set(value: String) =
            builder.AddAttribute("Name2", value)

        interface IComponentImport with
            member _.Builder = builder

    type HelloWorldFSharp with
        static member inline Render(builder: BlazorBuilderCore, name: String) =
            builder.OpenComponent<HelloWorldFSharp>()
            builder.AddAttribute("Name", name)
            HelloWorldFSharp__Import(builder)

[<AutoOpen>]
module ComponentWithLoopFSharp__Import =
    open FSharpComponents

    type [<Struct; IsReadOnly>] ComponentWithLoopFSharp__Import(builder: BlazorBuilderCore) =

        interface IComponentImport with
            member _.Builder = builder

    type ComponentWithLoopFSharp with
        static member inline Render(builder: BlazorBuilderCore) =
            builder.OpenComponent<ComponentWithLoopFSharp>()
            ComponentWithLoopFSharp__Import(builder)

[<AutoOpen>]
module NestedComponentFSharp__Import =
    open FSharpComponents

    type [<Struct; IsReadOnly>] NestedComponentFSharp__Import(builder: BlazorBuilderCore) =

        interface IComponentImport with
            member _.Builder = builder

    type NestedComponentFSharp with
        static member inline Render(builder: BlazorBuilderCore) =
            builder.OpenComponent<NestedComponentFSharp>()
            NestedComponentFSharp__Import(builder)

[<AutoOpen>]
module CounterFSharp__Import =
    open FSharpComponents

    type [<Struct; IsReadOnly>] CounterFSharp__Import(builder: BlazorBuilderCore) =

        interface IComponentImport with
            member _.Builder = builder

    type CounterFSharp with
        static member inline Render(builder: BlazorBuilderCore) =
            builder.OpenComponent<CounterFSharp>()
            CounterFSharp__Import(builder)

[<AutoOpen>]
module MatrixFSharp__Import =
    open FSharpComponents
    open System

    type [<Struct; IsReadOnly>] MatrixFSharp__Import(builder: BlazorBuilderCore) =

        interface IComponentImport with
            member _.Builder = builder

    type MatrixFSharp with
        static member inline Render(builder: BlazorBuilderCore, data: Int32[][]) =
            builder.OpenComponent<MatrixFSharp>()
            builder.AddAttribute("Data", data)
            MatrixFSharp__Import(builder)
