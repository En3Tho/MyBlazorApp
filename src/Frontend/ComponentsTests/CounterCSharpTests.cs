using FSharpComponents;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.ComponentsAndPages.Components;

namespace ComponentsTests;

public class CounterFSharpTests : TestContext
{
    [Fact]
    public void CounterStartsAtZero()
    {
        // Arrange
        var cut = RenderComponent<CounterFSharp>();

        // Assert that content of the paragraph shows counter at zero
        cut.Find("h1").InnerHtml.MarkupMatches("""Current count: 0""");
    }

    [Fact]
    public void ClickingButtonIncrementsCounter()
    {
        // Arrange
        var cut = RenderComponent<CounterFSharp>();

        // Act - click button to increment counter
        cut.Find("button").Click();

        // Assert that the counter was incremented
        cut.Find("h1").InnerHtml.MarkupMatches("""Current count: 1""");
    }
}

public class CounterCSharpTests : TestContext
{
    public CounterCSharpTests()
    {
        Services
            .AddLogging()
            .AddSingleton<StateStorage>()
            .AddSingleton(new ThemeSwitch(Theme.Red));
    }

    [Fact]
    public void CounterStartsAtZero()
    {
        // Arrange
        var cut = RenderComponent<Counter>();

        // Assert that content of the paragraph shows counter at zero
        cut.Find("p").MarkupMatches("<p>Current count: 0</p>");
    }

    [Fact]
    public void ClickingButtonIncrementsCounter()
    {
        // Arrange
        var cut = RenderComponent<Counter>();

        // Act - click button to increment counter
        cut.Find("button").Click();

        // Assert that the counter was incremented
        cut.Find("p").MarkupMatches("<p>Current count: 1</p>");
    }
}