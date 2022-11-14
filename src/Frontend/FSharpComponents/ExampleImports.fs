module FSharpComponents.ExampleImports

open System
open System.Runtime.CompilerServices
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Core
open En3Tho.FSharp.ComputationExpressions.BlazorBuilder
open Microsoft.AspNetCore.Components
open TailwindComponents.Basics
open TailwindComponents.CodinGame

// Ideally this should be Auto-generated
module Imports =
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

open Imports

type OldWay() =
    inherit ComponentBase()
    override this.BuildRenderTree(builder) =
        builder.Render(blazor {
            let helloWorld = Unchecked.defaultof<HelloWorld>
            c<HelloWorld> {
                nameof(helloWorld.Name) => "C#"
                nameof(helloWorld.Name2) => "VB"
                class' "lol"
            }
        })

type Importer() =
    inherit ComponentBase()
    override this.BuildRenderTree(builder) =
        builder.Render(fun b ->
            HelloWorld.Render(b, "C#", Name2 = "VB") {
                class' "lol"
            }
        )

type MyRequiredImport() =
    inherit ComponentBase()

    let smallNum = IntInput<SmallNum>()

    override this.BuildRenderTree(builder) =

        let main = fragment {
            div {
                attributes {
                    class' "block h-12 w-36 bg-red-200"

                }
                input {
                    attributes {
                        class' "bg-red-200"
                        type' "number"
                        bindInput' (this, smallNum.Raw, smallNum.set_Raw)
                    }
                }
            }
        }

        let error = fragment {
            div {
                attributes {
                    class' "text-red-500"
                }

                "This field is required"
            }
        }

        builder.Render(fun b ->
            Required.Render(b, error, main, Func<bool>(fun () -> smallNum.IsValid))
        )

type FSharpComplexComponent() =
    inherit ComponentBase()

    member val MatrixData = [|
        [| 1; 1; 1; 1; 0; 0; 1; 0; 0; 3 |]
        [| 1; 1; 1; 0; 1; 0; 0; 1; 0; 2 |]
        [| 1; 1; 1; 0; 0; 1; 0; 0; 1; 1 |]
        [| 1; 0; 0; 1; 1; 1; 1; 0; 0; 2 |]
        [| 0; 1; 0; 1; 1; 1; 0; 1; 0; 2 |]
        [| 0; 0; 1; 1; 1; 1; 0; 0; 1; 2 |]
        [| 1; 0; 0; 1; 0; 0; 1; 1; 1; 3 |]
        [| 0; 1; 0; 0; 1; 0; 1; 1; 1; 2 |]
        [| 0; 0; 1; 0; 0; 1; 1; 1; 1; 3 |]
    |]

    override this.BuildRenderTree(builder) =
        let mutable x = 0
        let iterateColor() =
            x <- x + 1
            if x % 2 = 1 then "bg-violet-200" else "bg-violet-300"

        let divClass = "block h-4 "
        let renderFragment = fragment { span { "renderFragment: 123" } }
        let codeBlock = span { "codeBlock: 123" }
        let constant = "constant: 123"
        let template (value: string) = span { "template: "; value }

        builder.Render(blazor {
            div {
                attributes {
                    class' ^ divClass + iterateColor()
                }

                renderFragment
                div {
                    attributes {
                        class' ^ divClass + iterateColor()
                    }
                    codeBlock
                }
            }
            h3 {
                attributes {
                    class' ^ divClass + iterateColor()
                }

                "Fortresses"
                if Random.Shared.Next(0, 2) = 1 then
                    span { "Random span: 123" }
            }
            div {
                attributes {
                    class' ^ divClass + iterateColor()
                }

                constant
                div {
                    attributes {
                        class' ^ divClass + iterateColor()
                    }

                    template "123"
                    div {
                        attributes {
                            class' ^ divClass + iterateColor()
                        }

                        fun b ->
                            Matrix.Render(b, this.MatrixData)
                    }
                }
            }
        })