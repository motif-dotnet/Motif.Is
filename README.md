# Motif.Is

**Trial.** A proposition library for .NET specs, part of [Motif.NET](https://github.com/motif-dotnet). The idea and its origin story are in Motif.NET's [design/ideas.md](https://github.com/motif-dotnet/.github/blob/main/design/ideas.md#motifis-isly).

A spec states facts. Claims about values are extensions on the value; claims about code go through `Check.That`:

```csharp
order.Total.Is(11.25m);
order.Lines.HasCount(2);
Check.That(() => order.Place()).Throws<InvalidOperationException>();
```

A false claim fails with a sentence, following the exception's type name, which test runners print first:

```
Motif.Expected : `order.Placed` to be true
but it's false.
```

## Specs

Motif.Is's specs are written in Motif.Is. A few bootstrap specs, marked `// BOOTSTRAP`, use plain xUnit `Assert` to prove that a false claim throws and that `Throws` catches; everything else relies on those.

Spec names read as propositions across namespace, class and method, after Kevlin Henney: `A_claim.that_is_false.throws_Expected`, `An_order.cannot_be_placed.while_it_is_empty`.

`Deliberately_failing` holds specs that fail on purpose, to show the messages. Leave them out with:

```
dotnet test --filter "Kind!=FailureGallery"
```

## Plan: bootstrap from a released core

Once the basic claims settle, release a minimal core as an alpha NuGet package. The next version's specs then use that released package rather than the code they're checking, as a compiler is built by its previous release. That removes the circularity of self-hosting: a bug in the code under test can't hide itself, because the checks come from a fixed, known version. The hand-written bootstrap specs could then go.

Things to work out when we get there:

- **Two copies of the same names.** The package and the project both define `Motif.Is` and `Motif.Expected`. The specs would reference the package under an extern alias (`<PackageReference Include="Motif.Is" Aliases="released" />`) and use `released::Motif.Is` for their claims, which costs some readability.
- **Which claims go in the core.** Enough to check everything else: probably `Is`, `IsTrue`, `IsFalse` and `Throws`.
- **When to move the base forward.** Bumping the released core is a deliberate step, not every build.
