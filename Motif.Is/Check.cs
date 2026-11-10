using System.Runtime.CompilerServices;

namespace Motif.Is;

/// <summary>
/// Claims about code: <c>Check.That(() => order.Place()).Throws&lt;InvalidOperationException&gt;()</c>.
/// Claims about values are extensions on the value instead (<see cref="ValueClaims"/>).
/// </summary>
public static class Check
{
    public static Code That(Action code, [CallerArgumentExpression(nameof(code))] string subject = "") =>
        new(code, subject);

    /// <summary>
    /// For code that returns a value, e.g. <c>() => int.Parse("x")</c>, or a lambda held in a <c>var</c>,
    /// which C# types as a Func when it returns something. The value is ignored.
    /// </summary>
    public static Code That<TResult>(Func<TResult> code, [CallerArgumentExpression(nameof(code))] string subject = "") =>
        new(() => code(), subject);
}

/// <summary>Code that a spec makes a claim about.</summary>
public sealed class Code
{
    private readonly Action run;
    private readonly string subject;

    internal Code(Action run, string subject)
    {
        this.run = run;
        this.subject = subject;
    }

    /// <summary>Returns the exception, so a spec can make further claims about it.</summary>
    public TException Throws<TException>() where TException : Exception => Claims.Throws<TException>(run, subject);
}
