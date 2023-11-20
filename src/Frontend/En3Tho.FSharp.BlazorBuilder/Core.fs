namespace rec En3Tho.FSharp.BlazorBuilder.Core

open System
open System.Collections.Generic
open System.Runtime.CompilerServices
open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.Rendering

type BlazorBuilderCore(builder: RenderTreeBuilder) =
    inherit RenderTreeBlockBase()

    let mutable sequenceCount = 0

    member _.Builder with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        builder

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.GetSequenceCountForWhileExpression() =
        sequenceCount

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.ResumeSequenceAtStartOfWhileExpression(value) =
        sequenceCount <- value

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.Advance() =
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.AddContent(content: string) =
        builder.AddContent(sequenceCount, content)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.AddContent(content: RenderFragment) =
        builder.AddContent(sequenceCount, content)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.AddMarkupContent(content: string) =
        builder.AddMarkupContent(sequenceCount, content)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.OpenComponent<'a>() =
        builder.OpenComponent(sequenceCount, typeof<'a>)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.OpenElement(name: string) =
        builder.OpenElement(sequenceCount, name)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.AddAttribute(name: string) =
        builder.AddAttribute(sequenceCount, name)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.AddAttribute(name: string, value: obj) =
        builder.AddAttribute(sequenceCount, name, value)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.AddAttribute(name: string, value: bool) =
        builder.AddAttribute(sequenceCount, name, value)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.AddAttribute(name: string, value: string) =
        builder.AddAttribute(sequenceCount, name, value)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.AddAttribute(name: string, value: MulticastDelegate) =
        builder.AddAttribute(sequenceCount, name, value)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.AddAttribute(name: string, value: EventCallback) =
        builder.AddAttribute(sequenceCount, name, value)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.AddAttribute(name: string, value: EventCallback<'a>) =
        builder.AddAttribute(sequenceCount, name, value)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.AddComponentReferenceCapture(value: Action<obj>) =
        builder.AddComponentReferenceCapture(sequenceCount, value)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.CloseElement() =
        builder.CloseElement()

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.CloseComponent() =
        builder.CloseComponent()

type IAttribute =
    abstract RenderTo: builder: BlazorBuilderCore -> unit

type IElementName = // TODO: static
    abstract Name: string

type IAttributeName = // TODO: static
    abstract Name: string

type IComponentImport =
    abstract Builder: BlazorBuilderCore

// user for Builder.Advance calls
type BlazorBuilderZeroCode = delegate of BlazorBuilderCore -> unit

// div { markup }
type BlazorBuilderMarkupCode = delegate of BlazorBuilderCore -> unit

// div { attrs }
type BlazorBuilderAttributeCode = delegate of BlazorBuilderCore -> unit

// render<'comp> { ... } // Run()
type BlazorBuilderComponentCode = delegate of BlazorBuilderCore -> unit

// div { ... } // Run()
type BlazorBuilderElementCode = delegate of BlazorBuilderCore -> unit

// div { markup }
type BlazorBuilderChildContentCode = delegate of BlazorBuilderCore -> unit

// blazor { ... } // Run()
type BlazorBuilderBlazorCode = delegate of BlazorBuilderCore -> unit

// this is explicitly not a delegate because F# doesn't inline generic delegates
// https://github.com/dotnet/fsharp/issues/15326#issuecomment-1597485122
type ComponentImportCode<'a when 'a: struct and 'a :> IComponentImport> = BlazorBuilderCore -> 'a

type [<Struct; IsReadOnly>] Attribute<'name when 'name: struct and 'name :> IAttributeName> =
    member _.Name with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() = Unchecked.defaultof<'name>.Name

    interface IAttribute with
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        member this.RenderTo(builder) = builder.AddAttribute(this.Name)

type [<Struct; IsReadOnly>] Attribute<'name, 'value when 'name: struct and 'name :> IAttributeName>(value: 'value) =
    member _.Name with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() = Unchecked.defaultof<'name>.Name
    member _.Value with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() = value

    interface IAttribute with
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        member this.RenderTo(builder) = builder.AddAttribute(this.Name, this.Value)

type [<Struct; IsReadOnly>] CustomAttribute(name: string) =
    member _.Name with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() = name

    interface IAttribute with
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        member this.RenderTo(builder) = builder.AddAttribute(this.Name)

type [<Struct; IsReadOnly>] CustomAttribute<'a>(name: string, value: 'a) =
    member _.Name with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() = name
    member _.Value with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() = value

    interface IAttribute with
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        member this.RenderTo(builder) = builder.AddAttribute(this.Name, this.Value)

type [<Struct; IsReadOnly>] GetBuilderIntrinsic = struct end

// It's interesting to note that such overload complexity is actually a compiler benchmark
// Maybe it's worth being passed to compiler team or something
type BlazorElementOrComponentBuilderBase() =
    member inline this.Combine([<InlineIfLambda>] first: BlazorBuilderMarkupCode, [<InlineIfLambda>] second: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    member inline this.Combine([<InlineIfLambda>] first: BlazorBuilderZeroCode, [<InlineIfLambda>] second: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    member inline this.Combine([<InlineIfLambda>] first: BlazorBuilderElementCode, [<InlineIfLambda>] second: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    member inline this.Combine([<InlineIfLambda>] first: BlazorBuilderComponentCode, [<InlineIfLambda>] second: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    member inline this.Combine([<InlineIfLambda>] first: BlazorBuilderAttributeCode, [<InlineIfLambda>] second: BlazorBuilderAttributeCode) : BlazorBuilderAttributeCode =
        BlazorBuilderAttributeCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    member inline this.Combine([<InlineIfLambda>] first: BlazorBuilderZeroCode, [<InlineIfLambda>] second: BlazorBuilderAttributeCode) : BlazorBuilderAttributeCode =
        BlazorBuilderAttributeCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    // This is needed to preserve correct sequence counts in RenderTreeBuilder in case of non-yield operations (eg if ... then ...)
    // I'm not sure how does this work with while or for
    // maybe this isn't really needed? Need to check out diff mechanism
    member inline _.Zero() : BlazorBuilderZeroCode = BlazorBuilderZeroCode(fun builder -> builder.Advance())

    member inline this.Delay([<InlineIfLambda>] delay: unit -> BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun (builder) -> (delay()).Invoke(builder))

    member inline this.Delay([<InlineIfLambda>] delay: unit -> BlazorBuilderElementCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun (builder) -> (delay()).Invoke(builder))

    member inline this.Delay([<InlineIfLambda>] delay: unit -> BlazorBuilderComponentCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun (builder) -> (delay()).Invoke(builder))

    member inline this.Delay([<InlineIfLambda>] delay: unit -> BlazorBuilderAttributeCode) : BlazorBuilderAttributeCode =
        BlazorBuilderAttributeCode(fun (builder) -> (delay()).Invoke(builder))

    member inline this.Delay([<InlineIfLambda>] delay: unit -> BlazorBuilderZeroCode) : BlazorBuilderZeroCode =
        BlazorBuilderZeroCode(fun (builder) -> (delay()).Invoke(builder))

    member inline _.Yield<'attr when 'attr: struct and 'attr :> IAttribute>(attr: 'attr) : BlazorBuilderAttributeCode =
        BlazorBuilderAttributeCode(fun builder ->
            attr.RenderTo builder)

    // from a razor compiler point of view this is still wrong because sequence numbers can't be correctly assigned
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

type BlazorBuilderGenericMarkupBase() =

    member inline this.Combine([<InlineIfLambda>] first: BlazorBuilderMarkupCode, [<InlineIfLambda>] second: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    member inline _.Zero() : BlazorBuilderMarkupCode = BlazorBuilderMarkupCode(fun builder -> builder.Advance())

    member inline this.Delay([<InlineIfLambda>] delay: unit -> BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun (builder) -> (delay()).Invoke(builder))

type BlazorRunnerBase() =
    inherit BlazorBuilderGenericMarkupBase()

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

type RenderTreeBlockBase() =
    inherit BlazorRunnerBase()

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            codeBuilderCode.Invoke builder
            builder.CloseElement())

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderChildContentCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            codeBuilderCode.Invoke builder
            builder.CloseComponent())

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderComponentCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            codeBuilderCode.Invoke builder)

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderElementCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            codeBuilderCode.Invoke builder)

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: ComponentImportCode<_>) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            codeBuilderCode builder |> ignore
            builder.CloseComponent())

    member inline _.Yield(fragment: RenderFragment) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            builder.AddContent(fragment))

    member inline _.Yield(content: string) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            builder.AddContent(content: string))

    member inline _.Yield(markup: MarkupString) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            builder.AddMarkupContent(markup.Value))