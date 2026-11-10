namespace Motif.Is;

/// <summary>
/// Records the propositions a spec states, true or false, for whatever wants to read them
/// (one day: plain-English specs). Recording is per async flow, so parallel specs don't mix.
/// </summary>
public static class Propositions
{
    private static readonly AsyncLocal<PropositionRecording?> Current = new();

    /// <summary>Starts recording every proposition stated from here on, in this flow.</summary>
    public static PropositionRecording Record() => Current.Value = new PropositionRecording();

    internal static void Add(Proposition proposition) => Current.Value?.Add(proposition);

    internal static void Stop(PropositionRecording recording)
    {
        if (Current.Value == recording) Current.Value = null;
    }
}

public sealed class PropositionRecording : IDisposable
{
    private readonly List<Proposition> stated = [];
    private bool stopped;

    internal void Add(Proposition proposition)
    {
        if (!stopped) stated.Add(proposition);
    }

    /// <summary>Stops recording and returns what was stated.</summary>
    public IReadOnlyList<Proposition> Stop()
    {
        stopped = true;
        Propositions.Stop(this);
        return stated;
    }

    public void Dispose() => Stop();
}
