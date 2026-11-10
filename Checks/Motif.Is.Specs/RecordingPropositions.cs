namespace Motif.Is.Specs;

/// <summary>
/// The structure that plain-English specs would be generated from: every claim a spec states,
/// in either style, recorded as a proposition rather than only as a failure message.
/// </summary>
public sealed class RecordingPropositions
{
    [Fact]
    public void Both_styles_record_the_same_propositions()
    {
        var order = new Order();
        order.Add("Tea", 2, 3.50m);

        var recording = Propositions.Record();
        order.Total.Is(7.00m);
        Check.That(order.Total).Is(7.00m);
        order.Lines.HasCount(1);
        Check.That(order.Lines).HasCount(1);
        var stated = recording.Stop();

        stated.Select(p => p.Sentence).ToArray().Is(
        [
            "`order.Total` is 7.00",
            "`order.Total` is 7.00",
            "`order.Lines` has 1 item",
            "`order.Lines` has 1 item",
        ]);
    }

    [Fact]
    public void A_proposition_keeps_its_parts()
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
}
