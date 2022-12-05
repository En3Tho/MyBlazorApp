namespace FSharpComponents

open Microsoft.AspNetCore.Components
open En3Tho.FSharp.BlazorBuilder

[<Route("FullyFSharpPage")>]
type FullyFSharpPage() =
    inherit ComponentBase()

    // TODO: implement a custom mobile / desktop layout
    // only with F# components and markup

    override this.BuildRenderTree(builder) =
        builder.Render(blazor {
            div { class' "h-full flex flex-col gap-1" } {
                h1 { class' "text-center text-xl text-red-500" } {
                    "Hello from F#!"
                }
                div { class' "flex-1 flex bg-blue-500 my-2 place-content-center" } {
                    span { class' "my-auto" } {
                        "Middle!"
                    }
                }
                div { class' "mt-auto text-blue-500 text-center" } {
                    "Div at the bottom?"
                }
            }
        })