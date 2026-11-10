namespace Motif.Is;

/// <summary>
/// A claim about a subject, kept as structure rather than only as a message,
/// so that a generator can later turn specs into plain English.
/// </summary>
/// <param name="Subject">What the claim is about, as written in the spec, e.g. <c>order.Total</c>.</param>
/// <param name="Claim">The claim's verb phrase, e.g. <c>is</c>, <c>is greater than</c>, <c>throws</c>.</param>
/// <param name="Expected">The claim's object, shown as text, or null when the claim has none (<c>is null</c>).</param>
/// <param name="Holds">Whether the claim is true.</param>
/// <param name="Actual">What was found instead, as a phrase, e.g. <c>it is 12.49</c>.</param>
public sealed record Proposition(string Subject, string Claim, string? Expected, bool Holds, string Actual)
{
    /// <summary>The claim as a sentence: <c>`order.Total` is 12.50</c>.</summary>
    public string Sentence => Expected is null ? $"`{Subject}` {Claim}" : $"`{Subject}` {Claim} {Expected}";

    public override string ToString() => Holds ? Sentence : $"{Sentence}\nbut {Actual}";
}

/// <summary>Thrown when a spec states a proposition that is false.</summary>
public sealed class FalseProposition(Proposition proposition) : Exception(proposition.ToString())
{
    public Proposition Proposition { get; } = proposition;
}
