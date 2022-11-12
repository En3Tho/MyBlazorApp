namespace FSharpComponents

open System
open Microsoft.AspNetCore.Components
open TailwindComponents.CodinGame
open En3Tho.FSharp.ComputationExpressions.BlazorBuilder
open En3Tho.FSharp.Extensions

type Counter3() =
    inherit ComponentBase()
    override this.BuildRenderTree(builder) =
         builder.Render(blazor {
             h1 {
                 "Hello world!"
             }
        })

type Counter2() =
    inherit ComponentBase()
    let mutable clicks = 0
    let mutable incrementAmount = 1
    member this.OnClick() = clicks <- clicks + incrementAmount

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
                     bind' (this, incrementAmount, fun value -> incrementAmount <- value)
                 }
             }
         })

    member this.Test(builder) = this.BuildRenderTree(builder)

type Q() =
    inherit ComponentBase()
    member val Kek = "" with get, set
    override this.BuildRenderTree(builder) =
        builder.OpenElement(0, "input");
        builder.AddAttribute(1, "value", BindConverter.FormatValue(this.Kek));
        builder.AddAttribute(2, "onchange", EventCallback.Factory.CreateBinder(this, (fun __value -> this.Kek <- __value), this.Kek));
        builder.SetUpdatesAttributeName("value");
        builder.CloseElement();

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