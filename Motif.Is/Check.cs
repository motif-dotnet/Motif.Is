using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Motif.Is;

/// <summary>
/// Style 2: name the subject first, then make the claim. <c>Check.That(order.Total).Is(12.50m)</c>.
/// </summary>
public static class Check
{
    public static Subject<T> That<T>(T value, [CallerArgumentExpression(nameof(value))] string subject = "") =>
        new(value, subject);

    public static ActionSubject That(Action action, [CallerArgumentExpression(nameof(action))] string subject = "") =>
        new(action, subject);
}

/// <summary>
/// Covariant, so that a subject of <c>List&lt;int&gt;</c> is also a subject of <c>IEnumerable&lt;int&gt;</c>
/// and picks up the collection claims.
/// </summary>
public interface ISubject<out T>
{
    T Value { get; }
    string Name { get; }
}

public sealed class Subject<T>(T value, string name) : ISubject<T>
{
    public T Value { get; } = value;
    public string Name { get; } = name;

    public void Is(T expected) => Claims.Is(Value, expected, Name);

    public void IsNot(T other) => Claims.IsNot(Value, other, Name);

    public void IsNull() => Claims.IsNull(Value, Name);

    [return: NotNull]
    public T IsNotNull()
    {
        Claims.IsNotNull(Value, Name);
        return Value!;
    }
}

/// <summary>Claims that only make sense for some kinds of subject: booleans, comparables, collections.</summary>
public static class SubjectClaims
{
    public static void IsTrue(this ISubject<bool> subject) => Claims.Is(subject.Value, true, subject.Name);

    public static void IsFalse(this ISubject<bool> subject) => Claims.Is(subject.Value, false, subject.Name);

    public static void IsGreaterThan<T>(this ISubject<T> subject, T other) where T : IComparable<T> =>
        Claims.IsGreaterThan(subject.Value, other, subject.Name);

    public static void IsLessThan<T>(this ISubject<T> subject, T other) where T : IComparable<T> =>
        Claims.IsLessThan(subject.Value, other, subject.Name);

    public static void IsEmpty<T>(this ISubject<IEnumerable<T>> subject) => Claims.IsEmpty(subject.Value, subject.Name);

    public static void HasCount<T>(this ISubject<IEnumerable<T>> subject, int count) => Claims.HasCount(subject.Value, count, subject.Name);

    public static void Includes<T>(this ISubject<IEnumerable<T>> subject, T item) => Claims.Includes(subject.Value, item, subject.Name);
}

public sealed class ActionSubject(Action action, string name)
{
    public TException Throws<TException>() where TException : Exception => Claims.Throws<TException>(action, name);
}
