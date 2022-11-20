namespace En3Tho.FSharp.BlazorBuilder

open System
open System.Collections.Generic
open System.Runtime.CompilerServices
open En3Tho.FSharp.BlazorBuilder.Core
open Microsoft.AspNetCore.Components

[<Sealed>]
type ComponentBlock<'a when 'a :> ComponentBase>() =
    inherit BlazorBuilderBase()

    member inline _.Yield<'attr when 'attr: struct and 'attr :> IAttribute>(attr: 'attr) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            attr.RenderTo builder)

    static member val Instance = ComponentBlock<'a>()

    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode) : BlazorBuilderComponentCode =
        fun builder _ ->
            builder.OpenComponent<'a>()
            runExpr.Invoke builder
            builder.CloseComponent()

type ElementBlockBase<'name when 'name :> IElementName and 'name: struct>() =
    inherit BlazorBuilderBase()
    member _.Name = Unchecked.defaultof<'name>.Name

    member inline this.Yield<'attr, 'import when 'attr: struct and 'attr :> IAttribute and 'import :> IComponentImport>(_: 'import, attr: 'attr) =
        fun builder ->
            attr.RenderTo builder

    member inline _.Yield<'attr when 'attr: struct and 'attr :> IAttribute>(attr: 'attr) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            attr.RenderTo builder)

    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            builder.OpenElement(this.Name)
            runExpr.Invoke builder)

[<AbstractClass; Sealed; Extension>]
type ComponentImportBlock() =

    [<Extension>]
    static member inline Combine(_: #IComponentImport, [<InlineIfLambda>] first: BlazorBuilderCode, [<InlineIfLambda>] second: BlazorBuilderCode) : BlazorBuilderCode =
        BlazorBuilderCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    [<Extension>]
    static member inline Zero(_: #IComponentImport) : BlazorBuilderCode = BlazorBuilderCode(fun (builder) -> builder.Advance())

    [<Extension>]
    static member inline Delay(_: #IComponentImport, [<InlineIfLambda>] delay: unit -> BlazorBuilderCode) =
        fun (builder) -> (delay()).Invoke(builder)

    [<Extension>]
    static member inline Yield<'attr, 'import when 'attr: struct and 'attr :> IAttribute and 'import :> IComponentImport>(_: 'import, attr: 'attr) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            attr.RenderTo builder)

    [<Extension>]
    static member inline Run(this: #IComponentImport, [<InlineIfLambda>] runExpr: BlazorBuilderCode) =
        runExpr.Invoke this.Builder
        this

[<AbstractClass; Sealed; Extension>]
type BlazorBuilderCodeBlock() =

    [<Extension>]
    static member inline While([<InlineIfLambda>] this: BlazorBuilderCode, [<InlineIfLambda>] moveNext: unit -> bool, [<InlineIfLambda>] whileExpr: BlazorBuilderCode) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            let sequence = builder.GetSequenceCountForWhileExpression()
            while moveNext() do
                builder.ResumeSequenceAtStartOfWhileExpression(sequence)
                (whileExpr.Invoke(builder)))

    [<Extension>]
    static member inline Combine([<InlineIfLambda>] this: BlazorBuilderCode, [<InlineIfLambda>] first: BlazorBuilderCode, [<InlineIfLambda>] second: BlazorBuilderCode) : BlazorBuilderCode =
        BlazorBuilderCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    [<Extension>]
    static member inline TryFinally([<InlineIfLambda>] this: BlazorBuilderCode, [<InlineIfLambda>] tryExpr: BlazorBuilderCode, [<InlineIfLambda>] compensation: BlazorBuilderCode) =
        BlazorBuilderCode(fun (builder) ->
            try
                tryExpr.Invoke(builder)
            finally
                compensation.Invoke(builder))

    [<Extension>]
    static member inline TryWith([<InlineIfLambda>] this: BlazorBuilderCode, [<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: exn -> BlazorBuilderCode) : BlazorBuilderCode =
        BlazorBuilderCode(fun (builder) ->
            try
                tryExpr()
            with e ->
                (compensation e).Invoke(builder))

    [<Extension>]
    static member inline Using([<InlineIfLambda>] this: BlazorBuilderCode, resource: #IDisposable, [<InlineIfLambda>] tryExpr: #IDisposable -> BlazorBuilderCode) : BlazorBuilderCode =
        this.TryFinally(
            (fun (builder) -> (tryExpr(resource).Invoke(builder))),
            (fun (builder) -> if not (isNull (box resource)) then resource.Dispose()))

    [<Extension>]
    static member inline For([<InlineIfLambda>] this: BlazorBuilderCode, values: 'a seq, [<InlineIfLambda>] forExpr: 'a -> BlazorBuilderCode) : BlazorBuilderCode =
        this.Using(
            values.GetEnumerator(), (fun (e: IEnumerator<'a>) ->
                this.While((fun () -> e.MoveNext()), (fun (builder) ->
                    (forExpr e.Current).Invoke(builder))
                )
            )
        )

    [<Extension>]
    static member inline Zero([<InlineIfLambda>] this: BlazorBuilderCode) : BlazorBuilderCode = BlazorBuilderCode(fun (builder) -> builder.Advance())

    [<Extension>]
    static member inline Delay([<InlineIfLambda>] this: BlazorBuilderCode, [<InlineIfLambda>] delay: unit -> BlazorBuilderCode) : BlazorBuilderCode =
        BlazorBuilderCode(fun (builder) -> (delay()).Invoke(builder))
    
    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderCode, [<InlineIfLambda>] codeBuilderCode: BlazorBuilderCode) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            codeBuilderCode.Invoke builder
            builder.CloseElement())

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderCode, [<InlineIfLambda>] codeBuilderCode: ComponentImportCode<_>) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            codeBuilderCode builder |> ignore
            builder.CloseComponent())

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderCode, fragment: RenderFragment) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            builder.AddContent(fragment))

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderCode, content: string) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            builder.AddContent(content: string))

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderCode, markup: MarkupString) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            builder.AddMarkupContent(markup.Value))

    [<Extension>]
    static member inline Yield<'import when 'import: struct and 'import :> IComponentImport>([<InlineIfLambda>] this: BlazorBuilderCode, _: 'import) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            builder.CloseComponent())

    [<Extension>]
    static member inline Run([<InlineIfLambda>] this: BlazorBuilderCode, [<InlineIfLambda>] runExpr: BlazorBuilderCode) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            this.Invoke(builder)
            runExpr.Invoke(builder))

[<AutoOpen>]
module Extensions =
    type RenderTreeBlockBase with
        member inline _.Yield(_: ComponentBlock<'a>) : BlazorBuilderCode =
            BlazorBuilderCode(fun builder ->
                builder.OpenComponent<'a>()
                builder.CloseComponent())

[<Sealed>]
type BlazorBuilderRunner() =

    inherit RenderTreeBlockBase()
    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode) =
        fun builder ->
            let builder = BlazorBuilderCore(builder)
            runExpr.Invoke builder

[<Sealed>]
type RenderFragmentRunner() =

    inherit RenderTreeBlockBase()
    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode) : RenderFragment =
        RenderFragment(fun builder -> runExpr.Invoke (BlazorBuilderCore(builder)))