using Motif.Is;

namespace Motif;

/// <summary>
/// Thrown when a spec's claim is false. Test runners (xUnit v2 and v3, NUnit, MSTest) print an exception's
/// full type name before its message, so the name is chosen to start the sentence:
/// <c>Motif.Expected : `order.Placed` to be true</c> / <c>but it is false.</c>
/// </summary>
public sealed class Expected : Exception, IAssertionException
{
    internal Expected(Proposition proposition) : base(proposition.Failure) => Proposition = proposition;

    /// <summary>The false claim, as structure.</summary>
    public Proposition Proposition { get; }
}

/// <summary>
/// xUnit v3 treats an exception as an assertion failure (rather than an error in the spec) when it implements
/// an interface with this name, from any namespace. That way Motif.Is needs no reference to xUnit.
/// </summary>
internal interface IAssertionException;
