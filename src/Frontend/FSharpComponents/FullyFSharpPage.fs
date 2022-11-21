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
            h1 { class' "text-center text-xl text-red-500" } {
                "Hello from F#!"
            }
        })