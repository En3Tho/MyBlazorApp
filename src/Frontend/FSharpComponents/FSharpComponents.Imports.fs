// Auto-generated
namespace FSharpComponents
open System.Runtime.CompilerServices
open En3Tho.FSharp.BlazorBuilder.Core

module HelloWorldFSharpImport =
    open System

    type [<Struct; IsReadOnly>] HelloWorldFSharpImport(builder: BlazorBuilderCore) =

        member this.Name2 with set(value: String) =
            builder.AddAttribute("Name2", value)

        interface IComponentImport with
            member _.Builder = builder

    type HelloWorldFSharp with
        static member inline Render(builder: BlazorBuilderCore, name: String) =
            builder.OpenComponent<HelloWorldFSharp>()
            builder.AddAttribute("Name", name)
            HelloWorldFSharpImport(builder)

module ComponentWithLoopFSharpImport =

    type [<Struct; IsReadOnly>] ComponentWithLoopFSharpImport(builder: BlazorBuilderCore) =

        interface IComponentImport with
            member _.Builder = builder

    type ComponentWithLoopFSharp with
        static member inline Render(builder: BlazorBuilderCore) =
            builder.OpenComponent<ComponentWithLoopFSharp>()
            ComponentWithLoopFSharpImport(builder)

module NestedComponentFSharpImport =

    type [<Struct; IsReadOnly>] NestedComponentFSharpImport(builder: BlazorBuilderCore) =

        interface IComponentImport with
            member _.Builder = builder

    type NestedComponentFSharp with
        static member inline Render(builder: BlazorBuilderCore) =
            builder.OpenComponent<NestedComponentFSharp>()
            NestedComponentFSharpImport(builder)

module CounterFSharpImport =

    type [<Struct; IsReadOnly>] CounterFSharpImport(builder: BlazorBuilderCore) =

        interface IComponentImport with
            member _.Builder = builder

    type CounterFSharp with
        static member inline Render(builder: BlazorBuilderCore) =
            builder.OpenComponent<CounterFSharp>()
            CounterFSharpImport(builder)

module MatrixFSharpImport =
    open System

    type [<Struct; IsReadOnly>] MatrixFSharpImport(builder: BlazorBuilderCore) =

        interface IComponentImport with
            member _.Builder = builder

    type MatrixFSharp with
        static member inline Render(builder: BlazorBuilderCore, data: Int32[][]) =
            builder.OpenComponent<MatrixFSharp>()
            builder.AddAttribute("Data", data)
            MatrixFSharpImport(builder)

open HelloWorldFSharpImport
open ComponentWithLoopFSharpImport
open NestedComponentFSharpImport
open CounterFSharpImport
open MatrixFSharpImport

[<AbstractClass; Sealed>]
type ImportsAsMembers() =

    static member inline HelloWorldFSharp'(builder, name) = HelloWorldFSharp.Render(builder, name)

    static member inline ComponentWithLoopFSharp'(builder) = ComponentWithLoopFSharp.Render(builder)

    static member inline NestedComponentFSharp'(builder) = NestedComponentFSharp.Render(builder)

    static member inline CounterFSharp'(builder) = CounterFSharp.Render(builder)

    static member inline MatrixFSharp'(builder, data) = MatrixFSharp.Render(builder, data)
