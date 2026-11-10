namespace Motif.Is.Specs.Code_claimed_to_throw;

public sealed class holds
{
    // BOOTSTRAP: plain xUnit Assert.
    [Fact]
    public void when_it_throws_that_exception_and_hands_it_back()
    {
        var problem = new InvalidOperationException("nope");

        Assert.Same(problem, Check.That(() => throw problem).Throws<InvalidOperationException>());
    }

    [Fact]
    public void when_it_throws_that_exception()
    {
        Check.That(() => _ = 1 / int.Parse("0")).Throws<DivideByZeroException>();
    }
}

public sealed class is_false
{
    // BOOTSTRAP: plain xUnit Assert.
    [Fact]
    public void when_it_throws_nothing()
    {
        Assert.Throws<Expected>(() => Check.That(() => { }).Throws<InvalidOperationException>());
    }

    [Fact]
    public void when_it_throws_something_else_and_says_what()
    {
        // A lambda that returns a value, held in a var, is a Func: Check.That takes that too.
        var claiming = () => Check.That(() => int.Parse("x")).Throws<DivideByZeroException>();

        Check.That(claiming).Throws<Expected>().Message
            .Is("`int.Parse(\"x\")` to throw DivideByZeroException\nbut it threw FormatException: The input string 'x' was not in a correct format.");
    }
}
