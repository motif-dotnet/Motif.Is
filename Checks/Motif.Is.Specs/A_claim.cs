// Spec names read as propositions: namespace, class and method together make one sentence,
// e.g. "A claim that is false throws Expected". See design/ideas.md in Motif.NET.

namespace Motif.Is.Specs.A_claim;

// BOOTSTRAP. These use plain xUnit Assert, not Motif.Is, to prove what every other spec relies on.

public sealed class that_holds
{
    [Fact]
    public void is_silent()
    {
        var total = 12.50m;

        Assert.Null(Record.Exception(() => total.Is(12.50m)));
    }
}

public sealed class that_is_false
{
    [Fact]
    public void throws_Expected()
    {
        var total = 12.49m;

        Assert.Throws<Expected>(() => total.Is(12.50m));
    }

    [Fact]
    public void says_what_was_expected_and_what_was_found()
    {
        var total = 12.49m;

        var thrown = Assert.Throws<Expected>(() => total.Is(12.50m));

        Assert.Equal("`total` to be 12.50\nbut it's 12.49.", thrown.Message);
    }
}
