// For more information see https://aka.ms/fsharp-console-apps

open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running
open Microsoft.AspNetCore.Components.Rendering
open FSharpComponents
open MyBlazorApp.ComponentsAndPages.Components

// |   Method |     Mean |   Error |  StdDev | Code Size |   Gen0 | Allocated |
// |--------- |---------:|--------:|--------:|----------:|-------:|----------:|
// | RenderFS | 289.4 ns | 1.64 ns | 1.54 ns |     314 B | 0.0315 |     528 B |
// | RenderCS | 216.9 ns | 1.28 ns | 1.14 ns |   1,948 B | 0.0243 |     408 B |

[<MemoryDiagnoser; DisassemblyDiagnoser(filters = [||])>]
type RenderTreeBuilderBenchmark() =

    let builder = RenderTreeBuilder()

    [<Benchmark>]
    member _.RenderFS() =
        builder.Clear()
        CounterFSharp.Test(builder)

    [<Benchmark>]
    member _.RenderCS() =
        builder.Clear()
        CounterCSharp.Test(builder)

BenchmarkRunner.Run<RenderTreeBuilderBenchmark>() |> ignore