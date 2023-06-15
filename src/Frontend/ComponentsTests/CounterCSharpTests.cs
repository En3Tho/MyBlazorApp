using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.ComponentsAndPages.Components;

namespace ComponentsTests;

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