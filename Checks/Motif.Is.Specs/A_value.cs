namespace Motif.Is.Specs.A_value;

public sealed class is_what_it_equals
{
    [Fact]
    public void and_not_anything_else()
    {
        var total = 12.50m;

        total.Is(12.50m);
        total.IsNot(12.49m);
    }

    [Fact]
    public void item_by_item_when_it_is_a_sequence()
    {
        List<int> items = [1, 2, 3];

        items.Is([1, 2, 3]);
    }
}

public sealed class claimed_to_be_something_else
{
    [Fact]
    public void fails_saying_what_it_is()
    {
        var total = 12.49m;

        Check.That(() => total.Is(12.50m)).Throws<Expected>().Message.Is("`total` to be 12.50\nbut it's 12.49.");
    }
}

public sealed class is_null_or_not_null
{
    [Fact]
    public void and_not_null_hands_the_value_back()
    {
        string? nickname = null;
        string? name = "Ann";

        nickname.IsNull();
        name.IsNotNull().Length.Is(3);
    }
}

public sealed class that_is_a_bool
{
    [Fact]
    public void is_true_or_false()
    {
        var placed = true;
        var cancelled = false;

        placed.IsTrue();
        cancelled.IsFalse();
    }
}

public sealed class compares_with_another
{
    [Fact]
    public void as_greater_than_or_less_than()
    {
        var count = 3;

        count.IsGreaterThan(2);
        count.IsLessThan(4);
    }

    [Fact]
    public void and_a_false_comparison_says_what_it_is()
    {
        var count = 3;

        Check.That(() => count.IsGreaterThan(5)).Throws<Expected>().Message.Is("`count` to be greater than 5\nbut it's 3.");
    }
}
