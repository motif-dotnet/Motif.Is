using System.Collections;
using System.Globalization;

namespace Motif.Is;

/// <summary>
/// The one implementation of every claim. Both styles (extensions on the value, and Check.That)
/// are thin wrappers over these, so they always agree on meaning and messages.
/// </summary>
internal static class Claims
{
    internal static void Is<T>(T actual, T expected, string subject) =>
        State(subject, "is", Show(expected), AreEqual(actual, expected), $"it is {Show(actual)}");

    internal static void IsNot<T>(T actual, T other, string subject) =>
        State(subject, "is not", Show(other), !AreEqual(actual, other), $"it is {Show(actual)}");

    internal static void IsNull<T>(T actual, string subject) =>
        State(subject, "is null", null, actual is null, $"it is {Show(actual)}");

    internal static void IsNotNull<T>(T actual, string subject) =>
        State(subject, "is not null", null, actual is not null, "it is null");

    internal static void IsGreaterThan<T>(T actual, T other, string subject) where T : IComparable<T> =>
        State(subject, "is greater than", Show(other), actual is not null && actual.CompareTo(other) > 0, $"it is {Show(actual)}");

    internal static void IsLessThan<T>(T actual, T other, string subject) where T : IComparable<T> =>
        State(subject, "is less than", Show(other), actual is not null && actual.CompareTo(other) < 0, $"it is {Show(actual)}");

    internal static void IsEmpty<T>(IEnumerable<T> actual, string subject)
    {
        var items = actual.ToList();
        State(subject, "is empty", null, items.Count == 0, $"it has {Items(items.Count)}: {Show(items)}");
    }

    internal static void HasCount<T>(IEnumerable<T> actual, int count, string subject)
    {
        var items = actual.ToList();
        var found = items.Count == 0 ? "it is empty" : $"it has {Items(items.Count)}: {Show(items)}";
        State(subject, "has", Items(count), items.Count == count, found);
    }

    internal static void Includes<T>(IEnumerable<T> actual, T item, string subject)
    {
        var items = actual.ToList();
        State(subject, "includes", Show(item), items.Contains(item), $"it is {Show(items)}");
    }

    internal static TException Throws<TException>(Action action, string subject) where TException : Exception
    {
        var name = typeof(TException).Name;
        try
        {
            action();
        }
        catch (TException thrown)
        {
            State(subject, "throws", name, true, "");
            return thrown;
        }
        catch (Exception other)
        {
            State(subject, "throws", name, false, $"it threw {other.GetType().Name}: {other.Message}");
        }

        State(subject, "throws", name, false, "it threw nothing");
        throw new InvalidOperationException("Unreachable: a false proposition throws.");
    }

    private static void State(string subject, string claim, string? expected, bool holds, string actual)
    {
        var proposition = new Proposition(Subject(subject), claim, expected, holds, actual);
        Propositions.Add(proposition);
        if (!holds) throw new FalseProposition(proposition);
    }

    // A lambda's subject reads better without its "() =>".
    private static string Subject(string subject)
    {
        var trimmed = subject.Trim();
        return trimmed.StartsWith("() =>", StringComparison.Ordinal) ? trimmed[5..].Trim() : trimmed;
    }

    // Sequences (but not strings) compare item by item, so [1, 2] is [1, 2] whatever the collection type.
    private static bool AreEqual<T>(T actual, T expected) =>
        actual is IEnumerable a and not string && expected is IEnumerable e and not string
            ? a.Cast<object?>().SequenceEqual(e.Cast<object?>())
            : EqualityComparer<T>.Default.Equals(actual, expected);

    private static string Items(int count) => count == 1 ? "1 item" : $"{count} items";

    internal static string Show(object? value) => value switch
    {
        null => "null",
        string s => $"\"{s}\"",
        char c => $"'{c}'",
        bool b => b ? "true" : "false",
        IEnumerable items => ShowItems(items),
        IFormattable f => f.ToString(null, CultureInfo.InvariantCulture),
        _ => value.ToString() ?? value.GetType().Name,
    };

    private static string ShowItems(IEnumerable items)
    {
        var all = items.Cast<object?>().ToList();
        var shown = string.Join(", ", all.Take(10).Select(Show));
        return all.Count > 10 ? $"[{shown}, … {all.Count - 10} more]" : $"[{shown}]";
    }
}
