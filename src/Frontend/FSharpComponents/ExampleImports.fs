namespace FSharpComponents.ExampleImports

open System
open En3Tho.FSharp.BlazorBuilder
open FSharpComponents
open Microsoft.AspNetCore.Components
open TailwindComponents.Basics
open TailwindComponents.CodinGame

open Imports

type OldWay() =
    inherit ComponentBase()
    override this.BuildRenderTree(builder) =
        builder.Render(blazor {
            let helloWorld = Unchecked.defaultof<HelloWorldFSharp>

            c<HelloWorldFSharp> {
                nameof(helloWorld.Name) => "C#"
                nameof(helloWorld.Name2) => "VB"
                class' "lol"
            }
        })

type Importer() =
    inherit ComponentBase()
    override this.BuildRenderTree(builder) =
        builder.Render(blazor {
            fun b ->
                HelloWorldFSharp.Render(b, "C#", Name2 = "VB") {
                    class' "lol"
                }
        })

type RequiredImportFSharp() =
    inherit ComponentBase()

    let smallNum = IntInput<SmallNum>()

    override this.BuildRenderTree(builder) =

        let main = fragment {
            div { class' "block h-12 w-36 bg-red-200" } {
                input { class' "bg-red-200"
                        type' "number"
                        bindInput' (this, smallNum.Raw, smallNum.set_Raw) }
                }
            }

        let error = fragment {
            div { class' "text-red-500" } {
                "This field is required"
            }
        }

        builder.Render(blazor {
            fun b -> Required.Render(b, error, main, fun () -> smallNum.IsValid)
        })

type ComplexComponentFSharp() =
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
        let renderFragment = fragment { span { () } { "renderFragment: 123" } }
        let codeBlock = span { () } { "codeBlock: 123" }
        let constant = "constant: 123"
        let template (value: string) = span { () } { "template: "; value }

        builder.Render(blazor {
            div { class' (divClass + iterateColor()) } {
                renderFragment
                div { class' (divClass + iterateColor()) } {
                    codeBlock
                }
            }
            h3 { class' (divClass + iterateColor()) } {
                "Fortresses"
                if Random.Shared.Next(0, 2) = 1 then
                    span { () } { "Random span: 123" }
            }
            div { class' (divClass + iterateColor()) } {
                constant
                div { class' (divClass + iterateColor()) } {
                    template "123"
                    div { class' (divClass + iterateColor()) } {
                        fun b ->
                            Matrix.Render(b, this.MatrixData)
                    }
                }
            }
        })