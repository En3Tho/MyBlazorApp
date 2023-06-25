using FSharpComponents;
using FSharpComponents.ExampleImports;
using Microsoft.AspNetCore.Components.QuickGrid;
using MyBlazorApp.ComponentsAndPages.Pages;
using TailwindComponents.Data;

namespace ComponentsTests;

public class FSharpFunTests : TestContext
{
    public FSharpFunTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [Fact]
    public void TestThatFSharpFunRenders()
    {
        var cut = RenderComponent<FSharpFun>();
        Assert.Equal(1, cut.FindComponent<CounterFSharp>().RenderCount);
    }

    [Fact]
    public void TestThatQuickGridImportFSharpRenders()
    {
        var cut = RenderComponent<QuickGridImportFSharp>();
        Assert.Equal(1, cut.FindComponent<QuickGrid<Person>>().RenderCount);
    }
}