// For more information see https://aka.ms/fsharp-console-apps

open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running
open Microsoft.AspNetCore.Components.Rendering
open FSharpComponents
open MyBlazorApp.ComponentsAndPages.Components

// |   Method |     Mean |   Error |  StdDev | Code Size |   Gen0 |   Gen1 | Allocated |
// |--------- |---------:|--------:|--------:|----------:|-------:|-------:|----------:|
// | RenderFS | 331.4 ns | 2.01 ns | 1.78 ns |     328 B | 0.1240 | 0.0010 |   2.03 KB |
// | RenderCS | 245.6 ns | 1.76 ns | 1.56 ns |   1,962 B | 0.1121 | 0.0005 |   1.84 KB |

// I guess this is because of many AttributeStructures all around
// Maybe it's possible to swap them to something else?

[<MemoryDiagnoser; DisassemblyDiagnoser(1, false, false, true, false, false, false, [||])>]
type RenderTreeBuilderBenchmark() =
    [<Benchmark>]
    member _.RenderFS() =
        let builder = new RenderTreeBuilder()
        let counter2 = Counter2()
        counter2.Test(builder)

    [<Benchmark>]
    member _.RenderCS() =
        let builder = new RenderTreeBuilder()
        let counter2 = ForFSharp()
        counter2.Test(builder)

BenchmarkRunner.Run<RenderTreeBuilderBenchmark>() |> ignore