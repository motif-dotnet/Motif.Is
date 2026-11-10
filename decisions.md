# Motif.Is: decisions so far

Motif.Is is a proposition library for .NET specs: a spec states facts (`order.Total.Is(11.25m)`) rather than what things *should* be. The idea, its reason to exist and its origin story are in Motif.NET's [design/ideas.md](https://github.com/motif-dotnet/.github/blob/main/design/ideas.md#motifis-isly).

| # | Topic | Decision |
|---|---|---|
| 1 | Principle | Benefits to the library's users come first: claims read in a spec like good English propositions. Second, failures read well: clear first, and succinct where possible. The library's internals and its own specs come after those. |
| 2 | Name | `Motif.Is` for the repo, package and namespace. Isly, the original name (a play on Shouldly), is kept for the origin story. |
| 3 | Styles | Mixed, each where it reads better. Claims about values are extensions on the value: `order.Total.Is(11.25m)`. Claims about code go through `Check.That(() => order.Place()).Throws<…>()`, because an extension can't take a lambda directly. A first trial had both styles for everything; reading the specs side by side decided it. |
| 4 | One core | Every claim is implemented once, in `Claims`; the public surface is a thin layer over it. |
| 5 | Failures | The exception is `Motif.Expected`, because test runners print the full type name before the message, so the name starts the sentence: ``Motif.Expected : `order.Placed` to be true`` / `but it's false.` Checked with xUnit v2, xUnit v3, NUnit and MSTest: all print it (MSTest adds a "Test method … threw exception:" line first). "Expected … to be" over "is … but it is", and "it's" over "it is". Every failure ends with a full stop, unless what was found already ends with one. |
| 6 | Two forms of each claim | Each claim has a statement form (*is*, *has*, *throws*) for recorded sentences and an expectation form (*to be*, *to have*, *to throw*) for failures. |
| 7 | No test framework reference | Motif.Is works with any test framework, and references none. xUnit v3 treats an exception as an assertion failure when it implements an interface named `IAssertionException` from any namespace, so Motif.Is has its own, internally. |
| 8 | Subjects | A subject's name is its source text, through `CallerArgumentExpression`. For code, a lambda's `() =>` is dropped. |
| 9 | Code that returns a value | `Check.That` takes a `Func<T>` as well as an `Action`, ignoring the value. Without it, `var claiming = () => …` (a Func, because the lambda returns something) wouldn't compile with `Check.That`. |
| 10 | Sequences | `Is` compares two sequences (but not strings) item by item, whatever their collection types. |
| 11 | Propositions | Every claim, true or false, is recorded as a `Proposition` (subject, claim, expectation, value, holds, found) when a recording is running. That's the structure plain-English specs would be generated from. Recording is per async flow, so specs running in parallel don't mix. |
| 12 | Self-hosted specs | Motif.Is's specs are written in Motif.Is. A few bootstrap specs, marked `// BOOTSTRAP`, use plain xUnit `Assert` to prove that a false claim throws and that `Throws` catches. Breaking the core on purpose fails 9 specs, so the self-hosting isn't hollow. |
| 13 | Spec names | After Kevlin Henney: namespace, class and method together read as one proposition (`An_order.cannot_be_placed.while_it_is_empty`), and specs that share the start of a sentence share a namespace or class. Spec classes therefore have lower-case names, and `.editorconfig` turns off CS8981 for `Checks/`. Tried here first; it may become a Motif spec convention. |
| 14 | Failure gallery | `Deliberately_failing` holds specs that fail on purpose, to show the messages in a test runner. They're tagged `Kind=FailureGallery`; leave them out with `--filter "Kind!=FailureGallery"`, as CI does. |
| 15 | Not built the Motif way, yet | No sketch or solution plan: they describe apps, and a library has almost none of their parts. The repo has a glossary, decisions and spec conventions. Whether Motif's tools should cover a library is an open question this repo tests. |

## Later

- **Bootstrap from a released core.** Once the basic claims settle, release a minimal core as an alpha NuGet package, and spec the next version with it, as a compiler is built by its previous release. The checks then come from a fixed, known version, so a bug in the code under test can't hide itself, and the bootstrap specs could go. The package and project define the same names, so the specs would reference the package under an extern alias (`<PackageReference Include="Motif.Is" Aliases="released" />`). The core needs only enough to check the rest: probably `Is`, `IsTrue`, `IsFalse` and `Throws`. Moving to a newer released core is a deliberate step.
- **Multi-line strings in failures**, such as a claim about a message, print over several lines and are hard to read.
- **Marking bootstrap specs** by more than a comment, such as a trait.
- **The word for code claims.** `Check.That` might read better as something else, or with an optional `using static Motif.Is.Check;`.
- **Async code**: `Throws` for `Func<Task>`.
