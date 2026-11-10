namespace Motif.Is.Specs;

/// <summary>
/// The only specs not written in Motif.Is. They use plain xUnit Assert to prove the two things
/// every other spec relies on: a false claim throws, and Throws catches. Everything else is self-hosted.
/// </summary>
public sealed class Bootstrapping
{
    [Fact]
    public void A_true_claim_says_nothing()
    {
        var total = 12.50m;

        Assert.Null(Record.Exception(() => total.Is(12.50m)));
    }

    [Fact]
    public void A_false_claim_throws_and_says_what_was_found()
    {
        var total = 12.49m;

        var thrown = Assert.Throws<Expected>(() => total.Is(12.50m));

        Assert.Equal("`total` to be 12.50\nbut it's 12.49.", thrown.Message);
    }

    [Fact]
    public void Throws_returns_the_exception_when_it_is_thrown()
    {
        var problem = new InvalidOperationException("nope");
        Action failing = () => throw problem;

        Assert.Same(problem, failing.Throws<InvalidOperationException>());
    }

    [Fact]
    public void Throws_is_false_when_nothing_is_thrown()
    {
        Action fine = () => { };

        Assert.Throws<Expected>(() => fine.Throws<InvalidOperationException>());
    }
}
