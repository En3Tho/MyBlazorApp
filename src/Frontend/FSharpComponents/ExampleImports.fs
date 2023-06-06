namespace FSharpComponents.ExampleImports

open System
open System.Linq.Expressions
open En3Tho.FSharp.BlazorBuilder
open FSharpComponents
open Microsoft.AspNetCore.Components
open TailwindComponents
open TailwindComponents.Data
open TailwindComponents.Basics
open TailwindComponents.CodinGame
open TailwindComponentsImports
open Microsoft.AspNetCore.Components.QuickGrid
open System.Linq

[<Sealed>]
type OldWay() =
    inherit FSharpComponentBase()
    override this.BuildRenderTreeCore(builder) =
        builder {
            let helloWorld = Unchecked.defaultof<HelloWorldFSharp>

            render<HelloWorldFSharp> {
                nameof(helloWorld.Name) => "C#"
                nameof(helloWorld.Name2) => "VB"
            }
        }

[<Sealed>]
type Importer() =
    inherit FSharpComponentBase()
    override this.BuildRenderTreeCore(builder) =
        builder {
            HelloWorldFSharp.Render(builder, "C#", Name2 = "VB")
        }

[<Sealed>]
type RequiredImportFSharp() =
    inherit FSharpComponentBase()

    let smallNum = IntInput<SmallNum>()

    override this.BuildRenderTreeCore(builder) =

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

        builder {
            Required.Render(builder, error, main, fun () -> smallNum.IsEmpty || smallNum.IsValid)
        }

[<Sealed>]
type ComplexComponentFSharp() =
    inherit FSharpComponentBase()

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

    override this.BuildRenderTreeCore(builder) =
        let mutable x = 0
        let iterateColor() =
            x <- x + 1
            if x % 2 = 1 then "bg-violet-200" else "bg-violet-300"

        let block = "block h-4"
        let renderFragment = fragment { span { "renderFragment: 123" } }
        let codeBlock = span { "codeBlock: 123" }
        let constant = "constant: 123"
        // this is inlined unlike RenderFragment
        let inline template (value: string) = span { "template: "; value }

        builder {
            div { class' (block, iterateColor()) } {
                renderFragment
                div { class' (block, iterateColor()) } {
                    codeBlock
                }
            }
            h3 { class' (block, iterateColor()) } {
                "Fortresses"
                if Random.Shared.Next(0, 2) = 1 then
                    span { "Random span: 123" }
            }
            div { class' (block, iterateColor()) } {
                constant
                div { class' (block, iterateColor()) } {
                    template "123"
                    div { class' (block, iterateColor()) } {
                        Matrix.Render(builder, this.MatrixData)
                    }
                }
            }
        }

[<Sealed>]
type UsesRef() =
    inherit FSharpComponentBase()
    member val Ref = Unchecked.defaultof<HelloWorldFSharp> with get, set
    override this.BuildRenderTreeCore(builder) =
        builder {
            HelloWorldFSharp.Render(builder, "Blazor") {
                ref' this.set_Ref
            }
        }

[<Sealed>]
type QuickGridImportFromCSharp() =
    inherit FSharpComponentBase()
    override this.BuildRenderTreeCore(builder) =
        builder {
            render<QuickGridTest> {
                "Data" => [|
                    Person("John", "Doe", "email")
                    Person("Jane", "Doe", "email")
                    Person("John", "Smith", "email")
                    Person("Jane", "Smith", "email")
                |].AsQueryable()
            }
        }

[<Sealed>]
type QuickGridImportFSharp() =
    inherit FSharpComponentBase()
    override this.BuildRenderTreeCore(builder) =

        let cols = fragment {
            PropertyColumn.Render(builder, fun (p: Person) -> p.Name)
            PropertyColumn.Render(builder, fun (p: Person) -> p.Email)
            PropertyColumn.Render(builder, fun (p: Person) -> p.ImageUrl)
        }

        let data =
            [|
                Person("John", "Doe", "email")
                Person("Jane", "Doe", "email")
                Person("John", "Smith", "email")
                Person("Jane", "Smith", "email")
            |].AsQueryable()

        builder {
            QuickGrid.Render(builder, Items = data, ChildContent = cols)
        }

// TODO: how to make this syntax work?
// type QuickGridImportFSharpX() =
//     inherit ComponentBase()
//     override this.BuildRenderTree(builder) =
//
//         let data =
//             [|
//                 Person("John", "Doe", "email")
//                 Person("Jane", "Doe", "email")
//                 Person("John", "Smith", "email")
//                 Person("Jane", "Smith", "email")
//             |].AsQueryable()
//
//         builder.Render(blazor {
//             QuickGrid'(Items = data) {
//                 TemplateColumn' {
//                     PropertyColumn'(fun (p: Person) -> p.Name)
//                 }
//                 PropertyColumn'(fun (p: Person) -> p.Email)
//                 PropertyColumn'(fun (p: Person) -> p.ImageUrl)
//             })
//         })

[<Sealed>]
type QuickGridImportFSharp3() =
    inherit FSharpComponentBase()

    member _.Quote(f: Expression<Func<'a, 'b>>) = f

    override this.BuildRenderTreeCore(builder) =

        let data =
            [|
                Person("John", "Doe", "email")
                Person("Jane", "Doe", "email")
                Person("John", "Smith", "email")
                Person("Jane", "Smith", "email")
            |].AsQueryable()

        builder {
            render<QuickGrid<Person>> {
                "Items" => data
            } {
                render<PropertyColumn<Person, string>> {
                    "Property" => this.Quote(fun (p: Person) -> p.Name)
                }
                render<PropertyColumn<Person, string>> {
                    "Property" => this.Quote(fun (p: Person) -> p.Email)
                }
                render<PropertyColumn<Person, string>> {
                    "Property" => this.Quote(fun (p: Person) -> p.ImageUrl)
                }
            }
        }