namespace FSharpComponents

open Microsoft.AspNetCore.Components
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions.BlazorBuilder
open Microsoft.AspNetCore.Components.Web

type HelloWorld() =
    inherit ComponentBase()

    [<Parameter; EditorRequired>]
    member val Name = "" with get, set

    [<Parameter>]
    member val Name2 = "F#" with get, set

    override this.BuildRenderTree(builder) =
         builder.Render(blazor {
             h1 {
                 $"Hello, {this.Name} and {this.Name2}!"
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
                "Counter: "; clicks.ToString() // benchmark: do not change to interpolation (+90-100 ns)
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
                    bindChange' (this, this.IncrementAmount, this.set_IncrementAmount)
                }
            }
        })

    static member Test(builder) =
        let counter = CounterFSharp()
        counter.BuildRenderTree(builder)

type MatrixFSharp() =
    inherit ComponentBase()

    [<Parameter; EditorRequired>]
    member val Data: int[][] = null with get, set
    member private _.Run() = ()
    member private _.OnInput(args: ChangeEventArgs) = ()
    member private _.OnKeyDown(args: KeyboardEventArgs) = ()


    override this.BuildRenderTree(builder) =
        let tableRow = "flex gap-4 hover:bg-violet-300 rounded-md px-2 ";
        let tableCell = "w-4 font-semibold text-xl text-end ";

        builder.Render(blazor {
            div {
                attributes {
                    class' "flex flex-col gap-4 max-w-sm"
                }

                div {
                    attributes {
                        class' "p-4 rounded-md bg-violet-200 w-max"
                    }

                    table {
                        tr {
                            attributes {
                                class' tableRow
                            }

                            td {
                                attributes {
                                    class' (tableCell + "invisible")
                                }

                                "0"
                            }

                            for colIdx = 0 to this.Data[0].Length - 1 do
                                td {
                                    attributes {
                                        class' (tableCell + "text-red-500")
                                    }

                                    colIdx.ToString()
                                }
                        }

                        for colIdx = 0 to this.Data.Length - 1 do
                            let row = this.Data.[colIdx]
                            tr {
                                attributes {
                                    class' tableRow
                                }

                                td {
                                    attributes {
                                        class' (tableCell + "text-red-500")
                                    }

                                    colIdx.ToString()
                                }

                                for rowIdx = 0 to row.Length - 1 do
                                    let data = row[rowIdx]
                                    td {
                                        attributes {
                                            class' (tableCell + "last:text-blue-300 " + if data = 0 then "text-gray-100" else "")
                                        }

                                        data.ToString()
                                    }
                            }
                    }
                }

            }
        })

        // TODO: this is neat
        // builder.Render(blazor {
        //     div { class' "flex flex-col gap-4 max-w-sm"; type' "number"; } {
        //       div { class' "p-4 rounded-md bg-violet-200 w-max"; } {
        //       }
        // }
        //         div { class' "p-4 rounded-md bg-violet-200 w-max"
        //             table {
        //                 tr { class' tableRow
        //                     td { class' (tableCell + "invisible")
        //                         "0"
        //                     }
        //                     for colIdx = 0 to this.Data[0].Length - 1 do
        //                         td { class' (tableCell + "text-red-500")
        //                             colIdx.ToString()
        //                         }
        //                 }
        //
        //                 for colIdx = 0 to this.Data.Length - 1 do
        //                     let row = this.Data.[colIdx]
        //                     tr { class' tableRow
        //                         td { class' (tableCell + "text-red-500")
        //                             colIdx.ToString()
        //                         }
        //                         for rowIdx = 0 to row.Length - 1 do
        //                             let data = row[rowIdx]
        //                             td { class' (tableCell + "last:text-blue-300 " + if data = 0 then "text-gray-100" else "")
        //                                 data.ToString()
        //                          }
        //                  }
        //             }
        //         }
        //
        //     }
        // })