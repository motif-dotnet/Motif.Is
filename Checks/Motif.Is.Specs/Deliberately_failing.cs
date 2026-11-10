namespace Motif.Is.Specs.Deliberately_failing;

// DELIBERATELY FAILING. These specs exist only to show what failures look like in a test runner
// (Visual Studio's Test Explorer, dotnet test).
// Every spec here fails on purpose. To run everything else:
//   dotnet test --filter "Kind!=FailureGallery"

[Trait("Kind", "FailureGallery")]
public sealed class claims
{
    [Fact]
    public void Is()
    {
        var order = new Order();
        order.Add("Tea", 2, 3.50m);

        order.Total.Is(7.50m);
    }

    [Fact]
    public void IsNot()
    {
        var status = "Placed";

        status.IsNot("Placed");
    }

    [Fact]
    public void Is_a_sequence()
    {
        List<int> items = [1, 2, 4];

        items.Is([1, 2, 3]);
    }

    [Fact]
    public void IsNull()
    {
        string? nickname = "Annie";

        nickname.IsNull();
    }

    [Fact]
    public void IsNotNull()
    {
        string? nickname = null;

        nickname.IsNotNull();
    }

    [Fact]
    public void IsTrue()
    {
        var order = new Order();

        order.Placed.IsTrue();
    }

    [Fact]
    public void IsGreaterThan()
    {
        var count = 3;

        count.IsGreaterThan(5);
    }

    [Fact]
    public void IsEmpty()
    {
        List<string> errors = ["Name is required", "Email is invalid"];

        errors.IsEmpty();
    }

    [Fact]
    public void HasCount_when_empty()
    {
        var order = new Order();

        order.Lines.HasCount(2);
    }

    [Fact]
    public void Includes()
    {
        List<string> tags = ["new", "sale"];

        tags.Includes("clearance");
    }

    [Fact]
    public void Includes_a_record()
    {
        var order = new Order();
        order.Add("Tea", 2, 3.50m);

        order.Lines.Includes(new OrderLine("Cake", 1, 4.25m));
    }

    [Fact]
    public void Throws_when_nothing_is_thrown()
    {
        var order = new Order();
        order.Add("Tea", 1, 3.50m);

        Check.That(() => order.Place()).Throws<InvalidOperationException>();
    }

    [Fact]
    public void Throws_when_something_else_is_thrown()
    {
        Check.That(() => int.Parse("x")).Throws<OverflowException>();
    }
}
