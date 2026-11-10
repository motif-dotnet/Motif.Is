namespace Motif.Is;

/// <summary>
/// A claim about a subject, kept as structure rather than only as a message,
/// so that a generator can later turn specs into plain English.
/// </summary>
/// <param name="Subject">What the claim is about, as written in the spec, e.g. <c>order.Total</c>.</param>
/// <param name="Claim">The claim as a statement, e.g. <c>is</c>, <c>is greater than</c>, <c>throws</c>.</param>
/// <param name="Expectation">The same claim after "expected", e.g. <c>to be</c>, <c>to be greater than</c>, <c>to throw</c>.</param>
/// <param name="Value">The claim's value, shown as text, or null when the claim has none (<c>is null</c>).</param>
/// <param name="Holds">Whether the claim is true.</param>
/// <param name="Found">What was found instead, as a phrase, e.g. <c>it's 12.49</c>.</param>
public sealed record Proposition(string Subject, string Claim, string Expectation, string? Value, bool Holds, string Found)
{
    /// <summary>The claim as a statement of fact: <c>`order.Total` is 12.50</c>.</summary>
    public string Sentence => $"`{Subject}` {Claim}{WithValue}";

    /// <summary>
    /// The failure, written to follow the exception's type name, <c>Motif.Expected</c>, which test runners print first:
    /// <c>`order.Total` to be 12.50</c> / <c>but it's 12.49.</c>
    /// </summary>
    public string Failure => $"`{Subject}` {Expectation}{WithValue}\nbut {Found}{FullStop(Found)}";

    public override string ToString() => Sentence;

    private string WithValue => Value is null ? "" : $" {Value}";

    // A found phrase can end with a quoted exception message that has its own full stop.
    private static string FullStop(string text) => text.EndsWith('.') || text.EndsWith('!') || text.EndsWith('?') ? "" : ".";
}
