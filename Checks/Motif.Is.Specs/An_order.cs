// An app-like example, written the way an app's specs would be: extensions for values, Check.That for code.

namespace Motif.Is.Specs.An_order;

public sealed class totals_its_lines
{
    [Fact]
    public void by_quantity_and_price()
    {
        var order = new Order();
        order.Add("Tea", 2, 3.50m);
        order.Add("Cake", 1, 4.25m);

        order.Lines.HasCount(2);
        order.Total.Is(11.25m);
    }
}

public sealed class can_be_placed
{
    [Fact]
    public void once_it_has_a_line()
    {
        var order = new Order();
        order.Add("Tea", 1, 3.50m);

        order.Place();

        order.Placed.IsTrue();
    }
}

public sealed class cannot_be_placed
{
    [Fact]
    public void while_it_is_empty()
    {
        var order = new Order();

        Check.That(() => order.Place()).Throws<InvalidOperationException>();
        order.Placed.IsFalse();
    }
}
