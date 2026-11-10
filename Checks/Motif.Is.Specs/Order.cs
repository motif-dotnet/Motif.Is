namespace Motif.Is.Specs;

// A small, app-like subject for the example specs (An_order) and the deliberately failing ones.

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
