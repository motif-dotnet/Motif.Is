using System.Runtime.CompilerServices;

namespace Motif.Is;

/// <summary>
/// Style 1: claims as extensions on the value. <c>order.Total.Is(12.50m)</c>.
/// The subject's name comes from the source text, via CallerArgumentExpression.
/// </summary>
public static class ValueClaims
{
    public static void Is<T>(this T actual, T expected, [CallerArgumentExpression(nameof(actual))] string subject = "") =>
        Claims.Is(actual, expected, subject);

    public static void IsNot<T>(this T actual, T other, [CallerArgumentExpression(nameof(actual))] string subject = "") =>
        Claims.IsNot(actual, other, subject);

    public static void IsNull<T>(this T actual, [CallerArgumentExpression(nameof(actual))] string subject = "") =>
        Claims.IsNull(actual, subject);

    /// <summary>Returns the value, now known not to be null, so a spec can carry on with it.</summary>
    public static T IsNotNull<T>(this T? actual, [CallerArgumentExpression(nameof(actual))] string subject = "") where T : class
    {
        Claims.IsNotNull(actual, subject);
        return actual!;
    }

    public static T IsNotNull<T>(this T? actual, [CallerArgumentExpression(nameof(actual))] string subject = "") where T : struct
    {
        Claims.IsNotNull(actual, subject);
        return actual!.Value;
    }

    public static void IsTrue(this bool actual, [CallerArgumentExpression(nameof(actual))] string subject = "") =>
        Claims.Is(actual, true, subject);

    public static void IsFalse(this bool actual, [CallerArgumentExpression(nameof(actual))] string subject = "") =>
        Claims.Is(actual, false, subject);

    public static void IsGreaterThan<T>(this T actual, T other, [CallerArgumentExpression(nameof(actual))] string subject = "") where T : IComparable<T> =>
        Claims.IsGreaterThan(actual, other, subject);

    public static void IsLessThan<T>(this T actual, T other, [CallerArgumentExpression(nameof(actual))] string subject = "") where T : IComparable<T> =>
        Claims.IsLessThan(actual, other, subject);

    public static void IsEmpty<T>(this IEnumerable<T> actual, [CallerArgumentExpression(nameof(actual))] string subject = "") =>
        Claims.IsEmpty(actual, subject);

    public static void HasCount<T>(this IEnumerable<T> actual, int count, [CallerArgumentExpression(nameof(actual))] string subject = "") =>
        Claims.HasCount(actual, count, subject);

    // Not "Contains": LINQ's Contains would win or clash, and silently return a bool nobody checks.
    public static void Includes<T>(this IEnumerable<T> actual, T item, [CallerArgumentExpression(nameof(actual))] string subject = "") =>
        Claims.Includes(actual, item, subject);

    /// <summary>Needs a variable of type Action: <c>Action placing = () => order.Place(); placing.Throws&lt;X&gt;();</c></summary>
    public static TException Throws<TException>(this Action action, [CallerArgumentExpression(nameof(action))] string subject = "") where TException : Exception =>
        Claims.Throws<TException>(action, subject);
}
