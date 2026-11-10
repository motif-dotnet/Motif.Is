namespace Motif.Is.Specs;

/// <summary>Style 1: the claim hangs off the value. Same cases as <see cref="ClaimsWithCheckThat"/>.</summary>
public sealed class ClaimsAsExtensions
{
    [Fact]
    public void A_value_is_what_it_equals()
    {
        var total = 12.50m;

        total.Is(12.50m);
        total.IsNot(12.49m);
    }

    [Fact]
    public void A_false_is_says_what_the_value_is_instead()
    {
        var total = 12.49m;
        Action claiming = () => total.Is(12.50m);

        claiming.Throws<Expected>().Message.Is("`total` to be 12.50\nbut it's 12.49.");
    }

    [Fact]
    public void Null_and_not_null()
    {
        string? nickname = null;
        string? name = "Ann";

        nickname.IsNull();
        name.IsNotNull().Length.Is(3);
    }

    [Fact]
    public void True_and_false()
    {
        var placed = true;
        var cancelled = false;

        placed.IsTrue();
        cancelled.IsFalse();
    }

    [Fact]
    public void Comparisons()
    {
        var count = 3;

        count.IsGreaterThan(2);
        count.IsLessThan(4);
    }

    [Fact]
    public void A_false_comparison_says_what_the_value_is()
    {
        var count = 3;
        Action claiming = () => count.IsGreaterThan(5);

        claiming.Throws<Expected>().Message.Is("`count` to be greater than 5\nbut it's 3.");
    }

    [Fact]
    public void Collections()
    {
        List<int> items = [1, 2, 3];
        List<int> none = [];

        items.HasCount(3);
        items.Includes(2);
        none.IsEmpty();
        items.Is([1, 2, 3]);
    }

    [Fact]
    public void A_false_count_shows_the_items()
    {
        List<int> items = [1, 2];
        Action claiming = () => items.HasCount(3);

        claiming.Throws<Expected>().Message.Is("`items` to have 3 items\nbut it has 2 items: [1, 2].");
    }

    [Fact]
    public void Throwing()
    {
        Action dividing = () => _ = 1 / int.Parse("0");

        dividing.Throws<DivideByZeroException>();
    }

    [Fact]
    public void A_false_throws_says_what_was_thrown_instead()
    {
        Action parsing = () => int.Parse("x");
        Action claiming = () => parsing.Throws<DivideByZeroException>();

        claiming.Throws<Expected>().Message
            .Is("`parsing` to throw DivideByZeroException\nbut it threw FormatException: The input string 'x' was not in a correct format.");
    }
}
