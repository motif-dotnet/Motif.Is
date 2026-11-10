namespace Motif.Is.Specs;

// A small, app-like example, so the two styles can be read side by side on something realistic.

public sealed record OrderLine(string Product, int Quantity, decimal Price);

public sealed class Order
{
    private readonly List<OrderLine> lines = [];

    public IReadOnlyList<OrderLine> Lines => lines;
    public decimal Total => lines.Sum(l => l.Quantity * l.Price);
    public bool Placed { get; private set; }

    public void Add(string product, int quantity, decimal price) => lines.Add(new(product, quantity, price));

    public void Place()
    {
        if (lines.Count == 0) throw new InvalidOperationException("An empty order can't be placed.");
        Placed = true;
    }
}

public sealed class PlacingAnOrder_as_extensions
{
    [Fact]
    public void An_order_totals_its_lines()
    {
        var order = new Order();
        order.Add("Tea", 2, 3.50m);
        order.Add("Cake", 1, 4.25m);

        order.Lines.HasCount(2);
        order.Total.Is(11.25m);
    }

    [Fact]
    public void A_placed_order_is_placed()
    {
        var order = new Order();
        order.Add("Tea", 1, 3.50m);

        order.Place();

        order.Placed.IsTrue();
    }

    [Fact]
    public void An_empty_order_cannot_be_placed()
    {
        var order = new Order();
        Action placing = order.Place;

        placing.Throws<InvalidOperationException>();
        order.Placed.IsFalse();
    }
}

public sealed class PlacingAnOrder_with_check_that
{
    [Fact]
    public void An_order_totals_its_lines()
    {
        var order = new Order();
        order.Add("Tea", 2, 3.50m);
        order.Add("Cake", 1, 4.25m);

        Check.That(order.Lines).HasCount(2);
        Check.That(order.Total).Is(11.25m);
    }

    [Fact]
    public void A_placed_order_is_placed()
    {
        var order = new Order();
        order.Add("Tea", 1, 3.50m);

        order.Place();

        Check.That(order.Placed).IsTrue();
    }

    [Fact]
    public void An_empty_order_cannot_be_placed()
    {
        var order = new Order();

        Check.That(() => order.Place()).Throws<InvalidOperationException>();
        Check.That(order.Placed).IsFalse();
    }
}
