namespace FSharpComponents

open System
open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.Web
open TailwindComponents.CodinGame
open En3Tho.FSharp.ComputationExpressions.ComponentBuilder
open En3Tho.FSharp.Extensions

type Fortresses() =
    inherit ComponentBase()
    member val MatrixData = Array.zeroCreate 0
    override this.BuildRenderTree(builder) =
        // builder {
            // div {
            // }
        // }

        builder.AddMarkupContent(0, "<div class=\"h4\">\r\n    123\r\n    <div class=\"h4\">\r\n        123\r\n    </div></div>\r\n");
        builder.AddMarkupContent(1, "<h3>Fortresses</h3>\r\n");
        builder.OpenElement(2, "div");
        builder.AddAttribute(3, "class", "h4");
        builder.AddMarkupContent(4, "\r\n    123\r\n    ");
        builder.OpenElement(5, "div");
        builder.AddAttribute(6, "class", "h4");
        builder.AddMarkupContent(7, "\r\n        123\r\n        ");
        builder.OpenElement(8, "div");
        builder.AddAttribute(9, "class", "h4")
        builder.OpenComponent<Matrix>(10)
        builder.AddAttribute(11, "Data", this.MatrixData);
        builder.CloseComponent();
        builder.CloseElement();
        builder.CloseElement();
        builder.CloseElement()

type Counter2() =
    inherit ComponentBase()
    let mutable clicks = 0
    member _.OnClick() = clicks <- clicks + 1
    override this.BuildRenderTree(builder) =

        html {
            h1 {
                $"Counter: {clicks}"
            }
            button {
                attributes {
                    class' "h-12 w-12 bg-blue-500 text-white rounded-full"
                    onClick' (this, this.OnClick)
                }

                "Click me"
            }
        } <| builder


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
        let matrix = Unchecked.defaultof<Matrix>
        let mutable x = 0
        let iterateColor() =
            x <- x + 1
            if x % 2 = 1 then "bg-violet-200" else "bg-violet-300"

        let divClass = "block h-4 "
        let renderFragment = fragment { span { "renderFragment: 123" } }
        let codeBlock = span { "codeBlock: 123" }
        let constant = "constant: 123"
        let template (value: string) = span { "template: "; value }

        html {
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

                        c<Matrix> {
                            attributes {
                                attr(nameof(matrix.Data), this.MatrixData)
                            }
                        }
                    }
                }
            }
        } ^ builder