// Specs use the mixed style: extensions for values, Check.That for code.
// These specs cover the other half of each style, so the library's whole surface stays checked.

namespace Motif.Is.Specs.Both_styles;

public sealed class make_the_same_claims
{
    [Fact]
    public void about_values_with_check_that()
    {
        var total = 12.50m;
        string? nickname = null;

        Check.That(total).Is(12.50m);
        Check.That(total).IsNot(12.49m);
        Check.That(nickname).IsNull();
        Check.That(Check.That("Ann").IsNotNull().Length).Is(3);
        Check.That(true).IsTrue();
        Check.That(false).IsFalse();
        Check.That(3).IsGreaterThan(2);
        Check.That(3).IsLessThan(4);
    }

    [Fact]
    public void about_collections_with_check_that()
    {
        List<int> items = [1, 2, 3];
        List<int> none = [];

        Check.That(items).HasCount(3);
        Check.That(items).Includes(2);
        Check.That(none).IsEmpty();
        Check.That(items).Is([1, 2, 3]);
    }

    [Fact]
    public void about_code_as_an_extension()
    {
        Action dividing = () => _ = 1 / int.Parse("0");

        dividing.Throws<DivideByZeroException>();
    }
}

public sealed class fail_with_the_same_message
{
    [Fact]
    public void about_a_value()
    {
        var total = 12.49m;
        Action asAnExtension = () => total.Is(12.50m);
        Action withCheckThat = () => Check.That(total).Is(12.50m);

        Check.That(withCheckThat).Throws<Expected>().Message.Is(Check.That(asAnExtension).Throws<Expected>().Message);
    }

    [Fact]
    public void about_code_named_the_same_way()
    {
        Action parsing = () => int.Parse("x");
        Action asAnExtension = () => parsing.Throws<DivideByZeroException>();
        Action withCheckThat = () => Check.That(parsing).Throws<DivideByZeroException>();

        Check.That(withCheckThat).Throws<Expected>().Message.Is(Check.That(asAnExtension).Throws<Expected>().Message);
    }
}

public sealed class record_the_same_propositions
{
    [Fact]
    public void for_the_same_claim()
    {
        var order = new Order();
        order.Add("Tea", 2, 3.50m);

        var recording = Propositions.Record();
        order.Total.Is(7.00m);
        Check.That(order.Total).Is(7.00m);
        var stated = recording.Stop();

        stated[1].Is(stated[0]);
    }
}
