module MyBlazorApp.Utility.Modules

open System
open System.Collections.Concurrent
open System.Collections.Generic
open System.Linq
open System.Reflection
open System.Reflection.Emit
open System.Runtime.ExceptionServices
open System.Threading.Tasks
open FSharp.NativeInterop
open Microsoft.FSharp.Reflection

#nowarn "0077"
#nowarn "0042"

module Core =
    let inline (^) f x = f x
    /// cast via op_Implicit
    let inline icast< ^a, ^b when (^a or ^b): (static member op_Implicit: ^a -> ^b)> (value: ^a): ^b = ((^a or ^b): (static member op_Implicit: ^a -> ^b) value)
    /// cast via op_Explicit
    let inline ecast< ^a, ^b when (^a or ^b): (static member op_Explicit: ^a -> ^b)> (value: ^a): ^b = ((^a or ^b): (static member op_Explicit: ^a -> ^b) value)
    /// unsafe cast like in C#
    let inline uсast<'a, 'b> (a: 'a): 'b = (# "" a: 'b #)

module Functions =
    let inline ignoreAndReturnDefault _ = Unchecked.defaultof<'a>
    let inline ignoreAndReturnTrue _ = true
    let inline ignoreAndReturnFalse _ = false
    let inline ignore1AndReturnValue value = fun _ -> value
    let inline ignore2AndReturnValue value = fun _ _ -> value
    let inline ignore3AndReturnValue value = fun _ _ _ -> value
    let inline valueFun value = fun _ -> value
    let inline wrap f x = fun() -> f x
    let inline wrap2 f x1 x2 = fun() -> f x1 x2
    let inline wrap3 f x1 x2 x3 = fun() -> f x1 x2 x3
    let inline invokeWith x f = f x
    let inline (&>>) f g = fun x -> f(); g x
    let inline (|>>) f g = fun x -> f x |> ignore; g x

module Object =
    let inline (&==) (a: ^a when ^a: not struct) b = Object.ReferenceEquals(a, b)
    let inline (&!=) (a: ^a when ^a: not struct) b = not (Object.ReferenceEquals(a, b))

    let inline ( *==) a b =
      (^a: (static member op_Equality: 'a * 'a -> bool) (a, b))

    let inline ( *!=) a b = not (a *== b)

    let inline private callIEquatableEquals<'a when 'a:> IEquatable<'a>> (a: 'a) (b: 'a) = a.Equals(b)

    let inline (^==) a b = callIEquatableEquals a b

    let inline (^!=) a b = not (a ^== b)

    let inline defaultValue def arg = if arg &== null then def else arg
    let inline defaultWith defThunk arg = if arg &== null then defThunk() else arg
    let inline nullCheck argName arg = if arg &== null then nullArg argName else arg

    let inline createNew<'a when 'a: (new : unit -> 'a)>() = new 'a()

module String =
    let inline nullOrWhiteSpaceCheck argName arg = if arg |> String.IsNullOrWhiteSpace then nullArg argName else arg
    let inline nullOrEmptyCheck argName arg = if arg |> String.IsNullOrEmpty then nullArg argName else arg
    let inline defaultValue def str = if String.IsNullOrEmpty str then def else str
    let inline defaultValueW def str = if String.IsNullOrWhiteSpace str then def else str

module Exception =
    let inline rethrow ex = ExceptionDispatchInfo.Throw ex; Unchecked.defaultof<'a>

module Printf =
    type T = T with
        static member inline ($) (T, arg: unit) = ()
        static member inline ($) (T, arg: int) = 0 // mandatory second terminal case; is unused in runtime but is required for the code to compile
        static member inline ($) (T, func: ^a -> ^b): ^a -> ^b = fun (_: 'a) -> T $ Unchecked.defaultof<'b>

    /// conditional ksprintf, ignores format on false and does not create any string
    let inline cksprintf runCondition stringFunc format: 'b =
        if runCondition then
           Printf.kprintf stringFunc format
        else
            T $ Unchecked.defaultof<'b>

module Option =
    let iterAsync (f: 'a -> Async<Unit>) opt =
        async { do! match opt with
                    | Some v -> f v
                    | None -> async.Zero() }
    let mapAsync f opt = async { return opt |> Option.map f }
    let bindAsync f opt = async { return opt |> Option.bind f }

module Async =
    let rec retryWhile condition retries work = async {
        let! result = work
        if condition result && retries > 0 then
            return! retryWhile condition (retries - 1) work
        else
            return result
    }

    let rec retryWhileWith condition delay retries work = async {
        let! result = work
        if condition result && retries > 0 then
            do! delay
            return! retryWhileWith condition delay (retries - 1) work
        else
            return result
    }

    let inline ofObj x = async { return x }

    let inline AwaitValueTask (vt: ValueTask<_>) =
        if vt.IsCompletedSuccessfully then vt.Result |> ofObj
        else vt.AsTask() |> Async.AwaitTask

module Byref =
    module Operators =
        let inline inc (a: 'a byref) = a <- a + LanguagePrimitives.GenericOne
        let inline dec (a: 'a byref) = a <- a - LanguagePrimitives.GenericOne
        let inline neg (a: 'a byref) = a <- (~-)a
        let inline (??<-) (a: 'a byref) v = if isNull a then a <- v
        let inline (???<-) (a: 'a byref) f = if isNull a then a <- f()
        let inline (+<-) (a: 'a byref) v = a <- a + v
        let inline (-<-) (a: 'a byref) v = a <- a - v
        let inline (/<-) (a: 'a byref) v = a <- a / v
        let inline (%<-) (a: 'a byref) v = a <- a % v
        let inline ( *<- ) (a: 'a byref) v = a <- a * v
        let inline (~~~) (a: 'a byref) = a <- ~~~a
        let inline (&&&<-) (a: 'a byref) v = a <- a &&& v
        let inline (|||<-) (a: 'a byref) v = a <- a ||| v
        let inline (^^^<-) (a: 'a byref) v = a <- a ^^^ v
        let inline (<<<<-) (a: 'a byref) v = a <- a <<< v
        let inline (>>><-) (a: 'a byref) v = a <- a >>> v
        let inline (&&<-) (a: bool byref) v = a <- a && v
        let inline (||<-) (a: bool byref) v = a <- a || v
        let inline ( **<- ) (a: 'a byref) v = a <- a ** v

    module Setters =
        /// all setters are reversed (byref is first parameter) because usually you want to write something into byref
        /// and pass (setv &ref or similar) into a function which then will write the value
        let inline setv (a: 'a byref) v = a <- v
        let inline setfn (a: 'a byref) f v = a <- f v
        let inline seti (a: 'a byref) v _ = a <- v
        let inline setTrue (a: bool byref) _ = a <- true
        let inline setFalse (a: bool byref) _ = a <- false
        let inline setZero (a: 'a byref) _ = a <- LanguagePrimitives.GenericZero
        let inline setOne (a: 'a byref) _ = a <- LanguagePrimitives.GenericOne
        let inline setMinusOne (a: 'a byref) _ = a <- -LanguagePrimitives.GenericOne
        let inline setParse (a: 'a byref) v = a <- (^a: (static member Parse: string -> 'a) (v))
        let inline defaultValue (a: 'a byref) v = if isNull a then a <- v
        let inline defaultWith (a: 'a byref) defThunk = if isNull a then a <- defThunk()

module EqualityComparer =
    [<Sealed>]
    type private MapEqualityComparer<'a, 'b when 'b: equality>(map: 'a -> 'b) =
        inherit EqualityComparer<'a>()
        override this.Equals(x, y) = map x = map y
        override this.GetHashCode obj = map obj |> hash

    [<Sealed>]
    type private DelegateEqualityComparer<'a>(eq, ghc) =
        inherit EqualityComparer<'a>()
        override this.Equals(x, y) = eq x y
        override this.GetHashCode obj = ghc obj

    type EqualityComparer<'a> with
        static member Create(eq, ghc) = DelegateEqualityComparer(eq, ghc):> EqualityComparer<'a>
        static member Create<'b when 'b: equality>(map: 'a -> 'b) = MapEqualityComparer(map):> EqualityComparer<'a>

module Array =
    let inline isNullOrEmpty (arr: 'a[]) = Object.ReferenceEquals(arr, null) || arr.Length = 0
    let inline defaultValue def arr = if isNullOrEmpty arr then def else arr
    let inline defaultWith defThunk arr = if isNullOrEmpty arr then defThunk() else arr

#nowarn "9"
module Span =
    let inline isEmpty (span: Span<_>) = span.Length = 0
    let inline any (span: Span<_>) = span.Length <> 0
    let inline slice start count (span: Span<_>) = span.Slice(start, count)
    let inline sliceFrom start (span: Span<_>) = span.Slice(start)
    let inline sliceTo index (span: Span<_>) = span.Slice(0, index)
    let inline fromPtr count (ptr: nativeptr<'a>) = Span<'a>(ptr |> NativePtr.toVoidPtr, count)
    let inline fromVoidPtr<'a when 'a: unmanaged> count ptr = Span<'a>(ptr, count)
    let inline fromArray (array: 'a[]): Span<_> = Span.op_Implicit array
    let inline fromArraySegment (array: 'a ArraySegment): Span<_> = Span.op_Implicit array
    let inline fromMemory (memory: Memory<_>) = memory.Span

module ReadOnlySpan =
    let inline isEmpty (span: ReadOnlySpan<_>) = span.Length = 0
    let inline any (span: ReadOnlySpan<_>) = span.Length <> 0
    let inline slice start count (span: ReadOnlySpan<_>) = span.Slice(start, count)
    let inline sliceFrom start (span: ReadOnlySpan<_>) = span.Slice(start)
    let inline sliceTo index (span: ReadOnlySpan<_>) = span.Slice(0, index)
    let inline fromPtr count (ptr: nativeptr<'a>) = ReadOnlySpan<'a>(ptr |> NativePtr.toVoidPtr, count)
    let inline fromVoidPtr<'a when 'a: unmanaged> count ptr = ReadOnlySpan<'a>(ptr, count)
    let inline fromString (str: string): ReadOnlySpan<char> = String.op_Implicit str
    let inline fromSpan (span: Span<_>): ReadOnlySpan<_> = Span.op_Implicit span
    let inline fromArray (array: 'a[]): ReadOnlySpan<_> = ReadOnlySpan.op_Implicit array
    let inline fromArraySegment (array: 'a ArraySegment): ReadOnlySpan<_> = ReadOnlySpan.op_Implicit array
    let inline fromMemory (memory: ReadOnlyMemory<_>) = memory.Span

module Memory =
    let inline isEmpty (memory: Memory<_>) = memory.Length = 0
    let inline any (memory: Memory<_>) = memory.Length <> 0
    let inline slice start count (memory: Memory<_>) = memory.Slice(start, count)
    let inline sliceFrom start (memory: Memory<_>) = memory.Slice(start)
    let inline sliceTo index (memory: Memory<_>) = memory.Slice(0, index)
    let inline fromArray (array: 'a[]): Memory<_> = Memory.op_Implicit array
    let inline fromArraySegment (array: 'a ArraySegment): Memory<_> = Memory.op_Implicit array

module ReadOnlyMemory =
    let inline isEmpty (memory: ReadOnlyMemory<_>) = memory.Length = 0
    let inline any (memory: ReadOnlyMemory<_>) = memory.Length <> 0
    let inline slice start count (memory: ReadOnlyMemory<_>) = memory.Slice(start, count)
    let inline sliceFrom start (memory: ReadOnlyMemory<_>) = memory.Slice(start)
    let inline sliceTo index (memory: ReadOnlyMemory<_>) = memory.Slice(0, index)
    let inline fromMemory (span: Memory<_>): ReadOnlyMemory<_> = Memory.op_Implicit span
    let inline fromArray (array: 'a[]): ReadOnlyMemory<_> = ReadOnlyMemory.op_Implicit array
    let inline fromArraySegment (array: 'a ArraySegment): ReadOnlyMemory<_> = ReadOnlyMemory.op_Implicit array

module Seq =
    open EqualityComparer
    let intersect left right = Enumerable.Intersect(left, right)
    let intersectBy<'a, 'b when 'b: equality> (map: 'a -> 'b) left right = Enumerable.Intersect(left, right, EqualityComparer<'a>.Create(map))
    let toResizeArray = Enumerable.ToList
    let ofType<'a> = Enumerable.OfType<'a>
    let isNotEmpty = Enumerable.Any
    let count = Enumerable.Count
    let inline toLookup (keySelector: 'a -> 'b) (elementSelector: 'a -> 'c) seq = Enumerable.ToLookup(seq, keySelector, elementSelector)

module Dictionary =
    let inline getValue key (d: Dictionary<'key, 'value>) =
        d.[key]

    let inline tryGetValue key (d: Dictionary<'key, 'value>) =
           match d.TryGetValue key with
           | true, value -> Some value
           | _ -> None

    let inline tryAdd key value (d: Dictionary<'key, 'value>) =
        d.TryAdd(key, value)

module ConcurrentQueue =
    let inline tryDequeue (cq: 'a ConcurrentQueue) =
        match cq.TryDequeue() with
        | true, value -> Some value
        | _ -> None

    let inline tryPeek (cq: 'a ConcurrentQueue) =
        match cq.TryPeek() with
        | true, value -> Some value
        | _ -> None

module ConcurrentDictionary =
    let inline getValue key (cd: ConcurrentDictionary<'key, 'value>) =
        cd.[key]

    let inline tryGetValue key (cd: ConcurrentDictionary<'key, 'value>) =
        match cd.TryGetValue key with
        | true, value -> Some value
        | _ -> None

    let inline tryRemove (key: 'key) (cd: ConcurrentDictionary<'key, 'value>) =
        match cd.TryRemove key with
        | true, value -> Some value
        | _ -> None

    let inline tryAdd key value (cd: ConcurrentDictionary<'key, 'value>) =
        cd.TryAdd(key, value)

module Func =
    let inline fromFun f = Func<'a, 'b> f
    let inline fromFun2 f = Func<'a, 'b, 'c> f
    let inline fromFun3 f = Func<'a, 'b, 'c, 'd> f

module Action =
    let inline fromFun f = Action<'a> f

/// Provides functions to get Union public fields which are not visible from F#
module Union =
    [<AbstractClass; Sealed>]
    type private TagGetter<'a>() =
       static member generateGetTag() =
            let parameterType = typeof<'a>
            let returnType = typeof<int>
            let tagPropertyInfo = parameterType.GetProperty("Tag", BindingFlags.Public ||| BindingFlags.Instance)
            // these check are delegate creation time only (static ctor)
            if parameterType |> FSharpType.IsUnion |> not
               // checks below are just to stay sure
               || isNull tagPropertyInfo
               || tagPropertyInfo.PropertyType <> returnType
               || isNull tagPropertyInfo.GetMethod then
                invalidOp <| sprintf "Invalid type specified: %s" parameterType.FullName
            else
                let dynamicMethod = DynamicMethod("GetTag", returnType, [| parameterType |])
                let generator = dynamicMethod.GetILGenerator()
                generator.Emit OpCodes.Ldarg_0
                generator.Emit(OpCodes.Call, tagPropertyInfo.GetMethod)
                generator.Emit OpCodes.Ret
                dynamicMethod.CreateDelegate typeof<Func<'a, int>> :?> Func<'a, int>

       [<DefaultValue>]
       static val mutable private _TagGetter: Func<'a, int>
       static do TagGetter<'a>._TagGetter <- TagGetter<'a>.generateGetTag()
       static member GetTag unionObj = TagGetter<'a>._TagGetter.Invoke unionObj

    [<AbstractClass; Sealed>]
    type private NameGetter<'a>() =

        [<DefaultValue>]
        static val mutable private _Names: string array
        static do NameGetter<'a>._Names <- [| for unionCase in typeof<'a> |> FSharpType.GetUnionCases -> unionCase.Name |]
        static member private GetNameInternal index = NameGetter<'a>._Names.[index]
        static member GetName<'a> (unionObj: 'a) = unionObj |> TagGetter.GetTag |> NameGetter<'a>.GetNameInternal

    /// Gets the value of "Tag" property of this union object
    let getTag unionObj = unionObj |> TagGetter.GetTag

    /// Gets the name of the union case of the underlying union object
    let getName unionObj = unionObj |> NameGetter<'a>.GetName