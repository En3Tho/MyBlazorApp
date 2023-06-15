using FSharpComponents;

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