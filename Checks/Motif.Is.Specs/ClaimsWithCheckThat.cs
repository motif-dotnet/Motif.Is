namespace Motif.Is.Specs;

/// <summary>Style 2: name the subject, then make the claim. Same cases as <see cref="ClaimsAsExtensions"/>.</summary>
public sealed class ClaimsWithCheckThat
{
    [Fact]
    public void A_value_is_what_it_equals()
    {
        var total = 12.50m;

        Check.That(total).Is(12.50m);
        Check.That(total).IsNot(12.49m);
    }

    [Fact]
    public void A_false_is_says_what_the_value_is_instead()
    {
        var total = 12.49m;

        var thrown = Check.That(() => Check.That(total).Is(12.50m)).Throws<Expected>();

        Check.That(thrown.Message).Is("`total` to be 12.50\nbut it is 12.49.");
    }

    [Fact]
    public void Null_and_not_null()
    {
        string? nickname = null;
        string? name = "Ann";

        Check.That(nickname).IsNull();
        Check.That(Check.That(name).IsNotNull().Length).Is(3);
    }

    [Fact]
    public void True_and_false()
    {
        var placed = true;
        var cancelled = false;

        Check.That(placed).IsTrue();
        Check.That(cancelled).IsFalse();
    }

    [Fact]
    public void Comparisons()
    {
        var count = 3;

        Check.That(count).IsGreaterThan(2);
        Check.That(count).IsLessThan(4);
    }

    [Fact]
    public void A_false_comparison_says_what_the_value_is()
    {
        var count = 3;

        var thrown = Check.That(() => Check.That(count).IsGreaterThan(5)).Throws<Expected>();

        Check.That(thrown.Message).Is("`count` to be greater than 5\nbut it is 3.");
    }

    [Fact]
    public void Collections()
    {
        List<int> items = [1, 2, 3];
        List<int> none = [];

        Check.That(items).HasCount(3);
        Check.That(items).Includes(2);
        Check.That(none).IsEmpty();
        Check.That(items).Is([1, 2, 3]);
    }

    [Fact]
    public void A_false_count_shows_the_items()
    {
        List<int> items = [1, 2];

        var thrown = Check.That(() => Check.That(items).HasCount(3)).Throws<Expected>();

        Check.That(thrown.Message).Is("`items` to have 3 items\nbut it has 2 items: [1, 2].");
    }

    [Fact]
    public void Throwing()
    {
        Check.That(() => _ = 1 / int.Parse("0")).Throws<DivideByZeroException>();
    }

    [Fact]
    public void A_false_throws_says_what_was_thrown_instead()
    {
        var thrown = Check.That(() => Check.That(() => int.Parse("x")).Throws<DivideByZeroException>()).Throws<Expected>();

        Check.That(thrown.Message)
            .Is("`int.Parse(\"x\")` to throw DivideByZeroException\nbut it threw FormatException: The input string 'x' was not in a correct format.");
    }
}
