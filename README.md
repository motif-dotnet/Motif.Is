# Motif.Is

A library in the **[Motif.NET](https://github.com/motif-dotnet)** suite. Specs state facts, not what things *should* be:

```csharp
order.Total.Is(11.25m);
order.Lines.HasCount(2);
Check.That(() => order.Place()).Throws<InvalidOperationException>();
```

Claims about values are extensions on the value; claims about code go through `Check.That`. A false claim fails with a sentence:

```
Motif.Expected : `order.Placed` to be true
but it's false.
```

Every claim is also kept as a proposition (its subject, claim and value), so that plain-English specs can one day be generated from well-named specs. That's the reason Motif.Is exists rather than just using an existing assertion library. The idea and its origin story, with credits to James Bach, Michael Bolton and Kevlin Henney, are in Motif.NET's [design/ideas.md](https://github.com/motif-dotnet/.github/blob/main/design/ideas.md#motifis-isly).

## Status

**Trial.** Not on NuGet, and not planned for now: alpha 1, the minimal core, will be served from a local feed in this repo, for Motif's own use ([decision 16](decisions.md), [#1](https://github.com/motif-dotnet/Motif.Is/issues/1)). It has the basic claims: `Is`, `IsNot`, `IsNull`, `IsNotNull`, `IsTrue`, `IsFalse`, `IsGreaterThan`, `IsLessThan`, `IsEmpty`, `HasCount`, `Includes`, and `Throws` for code. It works with any test framework: failures read the same in xUnit v2 and v3, NUnit and MSTest.

The decisions so far, and what's planned next, are in [decisions.md](decisions.md). The project's terms are in [motif/glossary.md](motif/glossary.md).

## Specs

Motif.Is's specs are written in Motif.Is. A few bootstrap specs, marked `// BOOTSTRAP`, use plain xUnit `Assert` to prove that a false claim throws and that `Throws` catches; everything else relies on those.

Spec names read as propositions across namespace, class and method, after Kevlin Henney: `A_claim.that_is_false.throws_Expected`, `An_order.cannot_be_placed.while_it_is_empty`.

`Deliberately_failing` holds specs that fail on purpose, to show the messages in a test runner.

## Build and run

You need the .NET 10 SDK.

```
dotnet test --filter "Kind!=FailureGallery"   # the specs
dotnet test --filter "Kind=FailureGallery"    # the failure gallery: every spec fails, on purpose
```

## Contributing

Bug reports, suggestions and pull requests are welcome; [CONTRIBUTING.md](https://github.com/motif-dotnet/.github/blob/main/CONTRIBUTING.md) says how. For ideas and questions, start a thread in [Discussions](https://github.com/motif-dotnet/.github/discussions).

## License

MIT, see [LICENSE](LICENSE).
