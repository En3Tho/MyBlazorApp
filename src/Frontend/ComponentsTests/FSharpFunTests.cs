using FSharpComponents;
using MyBlazorApp.ComponentsAndPages.Pages;

namespace ComponentsTests;

public class FSharpFunTests : TestContext
{
    [Fact]
    public void TestThatFSharpFunRenders()
    {
        var cut = RenderComponent<FSharpFun>();
        Assert.Equal(1, cut.FindComponent<CounterFSharp>().RenderCount);
    }
}