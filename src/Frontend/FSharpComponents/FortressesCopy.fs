namespace FSharpComponents

open System
open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.CompilerServices
open TailwindComponents.CodinGame
open En3Tho.FSharp.ComputationExpressions.ComponentBuilder

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
        html builder {
            div {
                class' => "h4"
                "123"
                div {
                    class' => "h4"
                    "123"
                }
            }
            h3 {
                "Fortresses"
            }
            div {
                class' => "h4"
                "123"
                div {
                    class' => "h4"
                    "123"
                    div {
                        class' => "h4"
                        c<Matrix> {
                            attr(nameof(matrix.Data), this.MatrixData)
                        }
                    }
                }
            }
        }