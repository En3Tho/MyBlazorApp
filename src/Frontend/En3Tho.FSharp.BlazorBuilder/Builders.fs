namespace En3Tho.FSharp.BlazorBuilder

open System
open System.Collections.Generic
open System.Runtime.CompilerServices
open En3Tho.FSharp.BlazorBuilder.Core
open Microsoft.AspNetCore.Components

[<Sealed>]
type ComponentBlock<'a when 'a :> ComponentBase>() =
    inherit BlazorBuilderGenericMarkupBase()

    static member val Instance = ComponentBlock<'a>()

    member inline _.Yield<'attr when 'attr: struct and 'attr :> IAttribute>(attr: 'attr) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            attr.RenderTo builder)

    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderMarkupCode) : BlazorBuilderComponentCode =
        BlazorBuilderComponentCode(fun builder ->
            builder.OpenComponent<'a>()
            runExpr.Invoke builder
            builder.CloseComponent())

[<Sealed>]
type ElementBlock<'name when 'name :> IElementName and 'name: struct>() =
    inherit BlazorElementOrComponentBuilderBase()
    member _.Name = Unchecked.defaultof<'name>.Name

    member inline _.Yield<'attr when 'attr: struct and 'attr :> IAttribute>(attr: 'attr) : BlazorBuilderAttributeCode =
        BlazorBuilderAttributeCode(fun builder ->
            attr.RenderTo builder)

    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderAttributeCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            builder.OpenElement(this.Name)
            runExpr.Invoke builder)


    member inline this.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            codeBuilderCode.Invoke builder
            builder.CloseElement())

    member inline this.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderComponentCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            codeBuilderCode.Invoke builder
            builder.CloseElement())

    member inline this.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderElementCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            codeBuilderCode.Invoke builder
            builder.CloseElement())

    member inline this.Yield([<InlineIfLambda>] codeBuilderCode: ComponentImportCode<_>) : BlazorBuilderMarkupCode=
        BlazorBuilderMarkupCode(fun builder ->
            codeBuilderCode builder |> ignore
            builder.CloseComponent())

    member inline this.Yield(fragment: RenderFragment) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            builder.AddContent(fragment))

    member inline this.Yield(content: string) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            builder.AddContent(content: string))

    member inline this.Yield(markup: MarkupString) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            builder.AddMarkupContent(markup.Value))

    member inline this.Yield<'import, 'dummy when 'import: struct and 'import :> IComponentImport>(_: 'import) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            builder.CloseComponent())


    member inline this.While([<InlineIfLambda>] moveNext: unit -> bool, [<InlineIfLambda>] whileExpr: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            let sequence = builder.GetSequenceCountForWhileExpression()
            while moveNext() do
                builder.ResumeSequenceAtStartOfWhileExpression(sequence)
                (whileExpr.Invoke(builder)))

    member inline this.TryFinally([<InlineIfLambda>] tryExpr: BlazorBuilderMarkupCode, [<InlineIfLambda>] compensation: BlazorBuilderMarkupCode) =
        BlazorBuilderMarkupCode(fun (builder) ->
            try
                tryExpr.Invoke(builder)
            finally
                compensation.Invoke(builder))

    member inline this.TryWith([<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: exn -> BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun (builder) ->
            try
                tryExpr()
            with e ->
                (compensation e).Invoke(builder))

    member inline this.Using(resource: #IDisposable, [<InlineIfLambda>] tryExpr: #IDisposable -> BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        this.TryFinally(
            (fun (builder) -> (tryExpr(resource).Invoke(builder))),
            (fun (builder) -> if not (isNull (box resource)) then resource.Dispose()))

    member inline this.For(values: 'a seq, [<InlineIfLambda>] forExpr: 'a -> BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        this.Using(
            values.GetEnumerator(), (fun (e: IEnumerator<'a>) ->
                this.While((fun () -> e.MoveNext()), (fun (builder) ->
                    (forExpr e.Current).Invoke(builder))
                )
            )
        )




    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderMarkupCode) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            builder.OpenElement(this.Name)
            runExpr.Invoke builder
            builder.CloseElement()) // TODO: why can't we close element here?


    // member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderZeroCode) : BlazorBuilderMarkupCode =
    //     BlazorBuilderMarkupCode(fun builder ->
    //         builder.OpenElement(this.Name)
    //         runExpr.Invoke builder)

[<AbstractClass; Sealed; Extension>]
type ComponentImportBlock() =

    [<Extension>]
    static member inline Combine(_: #IComponentImport, [<InlineIfLambda>] first: BlazorBuilderMarkupCode, [<InlineIfLambda>] second: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    [<Extension>]
    static member inline Zero(_: #IComponentImport) : BlazorBuilderMarkupCode = BlazorBuilderMarkupCode(fun (builder) -> builder.Advance())

    [<Extension>]
    static member inline Delay(_: #IComponentImport, [<InlineIfLambda>] delay: unit -> BlazorBuilderMarkupCode) =
        fun (builder) -> (delay()).Invoke(builder)

    [<Extension>]
    static member inline Yield<'attr, 'import when 'attr: struct and 'attr :> IAttribute and 'import :> IComponentImport>(_: 'import, attr: 'attr) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            attr.RenderTo builder)

    [<Extension>]
    static member inline Run(this: #IComponentImport, [<InlineIfLambda>] runExpr: BlazorBuilderMarkupCode) =
        runExpr.Invoke this.Builder
        this

[<AbstractClass; Sealed; Extension>]
type BlazorBuilderCodeBlock() =

    [<Extension>]
    static member inline While([<InlineIfLambda>] this: BlazorBuilderMarkupCode, [<InlineIfLambda>] moveNext: unit -> bool, [<InlineIfLambda>] whileExpr: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            let sequence = builder.GetSequenceCountForWhileExpression()
            while moveNext() do
                builder.ResumeSequenceAtStartOfWhileExpression(sequence)
                (whileExpr.Invoke(builder)))

    [<Extension>]
    static member inline Combine([<InlineIfLambda>] this: BlazorBuilderMarkupCode, [<InlineIfLambda>] first: BlazorBuilderMarkupCode, [<InlineIfLambda>] second: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    [<Extension>]
    static member inline TryFinally([<InlineIfLambda>] this: BlazorBuilderMarkupCode, [<InlineIfLambda>] tryExpr: BlazorBuilderMarkupCode, [<InlineIfLambda>] compensation: BlazorBuilderMarkupCode) =
        BlazorBuilderMarkupCode(fun (builder) ->
            try
                tryExpr.Invoke(builder)
            finally
                compensation.Invoke(builder))

    [<Extension>]
    static member inline TryWith([<InlineIfLambda>] this: BlazorBuilderMarkupCode, [<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: exn -> BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun (builder) ->
            try
                tryExpr()
            with e ->
                (compensation e).Invoke(builder))

    [<Extension>]
    static member inline Using([<InlineIfLambda>] this: BlazorBuilderMarkupCode, resource: #IDisposable, [<InlineIfLambda>] tryExpr: #IDisposable -> BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        this.TryFinally(
            (fun (builder) -> (tryExpr(resource).Invoke(builder))),
            (fun (builder) -> if not (isNull (box resource)) then resource.Dispose()))

    [<Extension>]
    static member inline For([<InlineIfLambda>] this: BlazorBuilderMarkupCode, values: 'a seq, [<InlineIfLambda>] forExpr: 'a -> BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        this.Using(
            values.GetEnumerator(), (fun (e: IEnumerator<'a>) ->
                this.While((fun () -> e.MoveNext()), (fun (builder) ->
                    (forExpr e.Current).Invoke(builder))
                )
            )
        )

    [<Extension>]
    static member inline Zero([<InlineIfLambda>] this: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode = BlazorBuilderMarkupCode(fun (builder) -> builder.Advance())

    [<Extension>]
    static member inline Delay([<InlineIfLambda>] this: BlazorBuilderMarkupCode, [<InlineIfLambda>] delay: unit -> BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun (builder) -> (delay()).Invoke(builder))

    // TODO: this is not right - need to find a set of common copy-paste extensions and just make proper typed delegates
    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderMarkupCode, [<InlineIfLambda>] codeBuilderCode: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            codeBuilderCode.Invoke builder
            builder.CloseElement())

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderMarkupCode, [<InlineIfLambda>] codeBuilderCode: BlazorBuilderComponentCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            codeBuilderCode.Invoke builder)

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderMarkupCode, [<InlineIfLambda>] codeBuilderCode: BlazorBuilderElementCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            codeBuilderCode.Invoke builder)

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderMarkupCode, fragment: RenderFragment) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            builder.AddContent(fragment))

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderMarkupCode, content: string) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            builder.AddContent(content: string))

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderMarkupCode, markup: MarkupString) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            builder.AddMarkupContent(markup.Value))

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderMarkupCode, [<InlineIfLambda>] codeBuilderCode: ComponentImportCode<_>) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            codeBuilderCode builder |> ignore
            builder.CloseComponent())

    [<Extension>]
    static member inline Yield<'import when 'import: struct and 'import :> IComponentImport>([<InlineIfLambda>] this: BlazorBuilderMarkupCode, _: 'import) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            builder.CloseComponent())

    [<Extension>]
    static member inline Run([<InlineIfLambda>] this: BlazorBuilderMarkupCode, [<InlineIfLambda>] runExpr: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            this.Invoke(builder)
            runExpr.Invoke(builder))

[<AutoOpen>]
module Extensions =
    type RenderTreeBlockBase with
        member inline _.Yield(_: ComponentBlock<'a>) : BlazorBuilderMarkupCode =
            BlazorBuilderMarkupCode(fun builder ->
                builder.OpenComponent<'a>()
                builder.CloseComponent())

[<Sealed>]
type BlazorBuilderRunner() =

    inherit RenderTreeBlockBase()
    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderMarkupCode) =
        fun builder ->
            let builder = BlazorBuilderCore(builder)
            runExpr.Invoke builder

[<Sealed>]
type RenderFragmentRunner() =

    inherit RenderTreeBlockBase()
    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderMarkupCode) : RenderFragment =
        RenderFragment(fun builder -> runExpr.Invoke (BlazorBuilderCore(builder)))