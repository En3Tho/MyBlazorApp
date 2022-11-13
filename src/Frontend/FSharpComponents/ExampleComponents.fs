namespace FSharpComponents

open System
open System.Runtime.CompilerServices
open En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Core
open Microsoft.AspNetCore.Components
open TailwindComponents.CodinGame
open En3Tho.FSharp.ComputationExpressions.BlazorBuilder
open En3Tho.FSharp.Extensions

type HelloWorld() =
    inherit ComponentBase()
    override this.BuildRenderTree(builder) =
         builder.Render(blazor {
             h1 {
                 "Hello world!"
             }
        })

type CounterFSharp() =
    inherit ComponentBase()
    let mutable clicks = 0 // maybe if these are properties it will get faster?
    member val IncrementAmount = 1 with get, set
    member this.OnClick() = clicks <- clicks + this.IncrementAmount

    override this.BuildRenderTree(builder) =
        builder.Render(blazor {
            h1 {
                "Counter: " + clicks.ToString() // benchmark: do not change to interpolation (+90-100 ns)
            }
            button {
                attributes {
                    class' "h-12 w-12 bg-blue-500 text-white rounded-full"
                    onClick' (this, this.OnClick)
                }

                "Click me"
            }
            input {
                attributes {
                    class' "h-12 w-12 bg-blue-500 text-red-500"
                    type' "number"
                    bind' (this, this.IncrementAmount, this.set_IncrementAmount)
                }
            }
        })

    member this.Test(builder) = this.BuildRenderTree(builder)

type Fortresses2() =
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

                        // yield! fun builder -> Matrix(builder, Data = this.MatrixData) // interesting but this required a wrapper
                        // it's a struct with 1 field which can be ignored later?
                        // fun b -> SomeComponent(b, Data = this.MatrixData, ChildContent = fragment { ... })
                        // or just SomeComponent(Data = ..., ChildContent = ...)
                        let matrix = Unchecked.defaultof<Matrix>
                        c<Matrix> {
                            attributes {
                                nameof(matrix.Data) => this.MatrixData
                            }
                        }
                    }
                }
            }
        })