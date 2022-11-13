// For more information see https://aka.ms/fsharp-console-apps

open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running
open Microsoft.AspNetCore.Components.Rendering
open FSharpComponents
open MyBlazorApp.ComponentsAndPages.Components

// |   Method |     Mean |   Error |  StdDev | Code Size |   Gen0 |   Gen1 | Allocated |
// |--------- |---------:|--------:|--------:|----------:|-------:|-------:|----------:|
// | RenderFS | 282.6 ns | 2.56 ns | 2.40 ns |     328 B | 0.1221 | 0.0005 |      2 KB |
// | RenderCS | 238.7 ns | 1.51 ns | 1.26 ns |   1,962 B | 0.1123 | 0.0007 |   1.84 KB |

// I guess this is because of many AttributeStructures all around
// Maybe it's possible to swap them to something else?

[<MemoryDiagnoser; DisassemblyDiagnoser(filters = [||])>]
type RenderTreeBuilderBenchmark() =
    [<Benchmark>]
    member _.RenderFS() =
        let builder = new RenderTreeBuilder()
        let counter2 = CounterFSharp()
        counter2.Test(builder)

    [<Benchmark>]
    member _.RenderCS() =
        let builder = new RenderTreeBuilder()
        let counter2 = CounterCSharp()
        counter2.Test(builder)

BenchmarkRunner.Run<RenderTreeBuilderBenchmark>() |> ignore