namespace FSharpComponents

open System.Collections.Generic
open Microsoft.AspNetCore.Components
open En3Tho.FSharp.BlazorBuilder
open Microsoft.AspNetCore.Components.Web

type HelloWorldFSharp() =
    inherit ComponentBase()

    [<Parameter; EditorRequired>]
    member val Name = "" with get, set

    [<Parameter>]
    member val Name2 = "F#" with get, set

    override this.BuildRenderTree(builder) =
         builder.Render(blazor {
             h1 {
                 $"Hello, {this.Name} from {this.Name2}!"
             }
        })

type ComponentWithLoopFSharp() =
    inherit ComponentBase()

    override this.BuildRenderTree(builder) =
        builder.Render(blazor {
            for i in 1..10 do
                h1 {
                    $"Hello, {i}!"
                }
        })

type ComponentWithLoopFSharp2() =
    inherit ComponentBase()

    override this.BuildRenderTree(builder) =
        builder.Render(blazor {
            for i in 1..10 do
                h1 {
                    div {
                        $"Hello, {i}!"
                    }
                }
        })

type NestedComponentFSharp() =
    inherit ComponentBase()

    override this.BuildRenderTree(builder) =
        builder.Render(blazor {
            // TODO: explore overload-based CE's here to skip {()} part?
            // At start yield can yield both attribute or child content
            // But attribute leads to a attribute code type
            // and child content leads to a content code type
            // running attribute code type will lead to a content code type
            // explore same idea with components so there can be a "natural" way to set child content
            // note that opening component should give a "fragment" ce, not be a part of overall blazor ce
            h1 {
                div {
                    "Hello"
                }
                div {
                    "Hello"
                }
            }
        })

type CounterFSharp() =
    inherit ComponentBase()
    let mutable clicks = 0
    member val IncrementAmount = 1 with get, set
    member this.OnClick() = clicks <- clicks + this.IncrementAmount

    override this.BuildRenderTree(builder) =

        builder.Render(blazor {
            h1 { class' "block" } {
                "Counter: "; clicks.ToString()
            }
            div { class' "flex gap-2" } {
                button { class' "block p-4 h-12 w-12 bg-blue-500 text-white rounded-full"
                         onClick' (this, this.OnClick) } {
                "Click me"
                }
                input {
                    class' "flex p-4 h-12 w-12 bg-blue-500 text-red-500 text-lg"
                    typeNumber'
                    bindChange' (this, this.IncrementAmount, this.set_IncrementAmount)
                }
            }
        })

    static member Test(builder) =
        let counter = CounterFSharp()
        counter.BuildRenderTree(builder)

type MatrixFSharp() =
    inherit ComponentBase()

    let mutable command: string = null
    let mutable error: string = null
    let history = List<string>()

    [<Parameter; EditorRequired>]
    member val Data: int[][] = null with get, set
    member private _.Run() = ()
    member private _.OnInput(args: ChangeEventArgs) = ()
    member private _.OnKeyDown(args: KeyboardEventArgs) = ()

    override this.BuildRenderTree(builder) =
        let tableRow = "flex gap-4 hover:bg-violet-300 rounded-md px-2";
        let tableCell = "w-4 font-semibold text-xl text-end";

        builder.Render(blazor {
            div { class' "flex flex-col gap-4 max-w-sm" } {
                div { class' "p-4 rounded-md bg-violet-200 w-max" } {
                    table {
                        colgroup {
                            col { class' "hover:bg-violet-300" }
                        }
                        tr { class' tableRow } {
                            td { class' (tableCell, "invisible") } {
                                "0"
                            }
                            for colIdx = 0 to this.Data[0].Length - 1 do
                                td { class' (tableCell, "text-red-500") } {
                                    colIdx.ToString()
                                }
                        }

                        for colIdx = 0 to this.Data.Length - 1 do
                            let row = this.Data[colIdx]
                            tr { class' tableRow } {
                                td { class' (tableCell, "text-red-500") } {
                                    colIdx.ToString()
                                }

                                for rowIdx = 0 to row.Length - 1 do
                                    let data = row[rowIdx]
                                    td { class' (tableCell, "last:text-blue-300", if data = 0 then "text-gray-100" else "") } {
                                        data.ToString()
                                    }
                            }
                    }
                }
            }
            div { class' "flex flex-col gap-2 w-full" } {
                div { class' "flex-1 gap-4 bg-slate-100 rounded-md" } {
                    input { class' "flex-1 p-4 bg-inherit w-full"
                            value' command
                            onInput' (this, this.OnInput)
                            onKeyDown' (this, this.OnKeyDown) }

                    if error <> null then
                        div { class' "text-red-500" } {
                            error
                        }

                    div { class' "flex flex-wrap py-4 w-full" } {
                        for item in history do
                            span { class' "w-[25%] bg-gray-100 text-center shadow p-4 rounded-md hover:bg-sky-200" } {
                                item
                            }
                    }
                }
            }
        })