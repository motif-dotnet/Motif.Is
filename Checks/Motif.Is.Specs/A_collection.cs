namespace Motif.Is.Specs.A_collection;

public sealed class has_a_count
{
    [Fact]
    public void of_its_items()
    {
        List<int> items = [1, 2, 3];

        items.HasCount(3);
    }

    [Fact]
    public void and_a_false_count_shows_the_items()
    {
        List<int> items = [1, 2];

        Check.That(() => items.HasCount(3)).Throws<Expected>().Message.Is("`items` to have 3 items\nbut it has 2 items: [1, 2].");
    }
}

public sealed class includes
{
    [Fact]
    public void each_of_its_items()
    {
        List<string> tags = ["new", "sale"];

        tags.Includes("new");
        tags.Includes("sale");
    }
}

public sealed class is_empty
{
    [Fact]
    public void when_it_has_no_items()
    {
        List<int> none = [];

        none.IsEmpty();
    }
}
