namespace Motif.Is.Specs.Every_claim;

/// <summary>The structure that plain-English specs would be generated from.</summary>
public sealed class is_recorded_as_a_proposition
{
    [Fact]
    public void with_its_parts()
    {
        var count = 3;

        var recording = Propositions.Record();
        count.IsGreaterThan(2);
        var stated = recording.Stop().Single();

        stated.Subject.Is("count");
        stated.Claim.Is("is greater than");
        stated.Expectation.Is("to be greater than");
        stated.Value.Is("2");
        stated.Holds.IsTrue();
    }

    [Fact]
    public void as_a_sentence()
    {
        var order = new Order();
        order.Add("Tea", 2, 3.50m);

        var recording = Propositions.Record();
        order.Total.Is(7.00m);
        order.Lines.HasCount(1);
        var stated = recording.Stop();

        stated.Select(p => p.Sentence).ToArray().Is(["`order.Total` is 7.00", "`order.Lines` has 1 item"]);
    }
}
