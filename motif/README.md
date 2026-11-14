# motif/

This repo uses the [Motif.NET](https://github.com/motif-dotnet) conventions. This folder holds their files, for people and coding agents alike.

| File | What it is | Who changes it |
|---|---|---|
| [glossary.md](glossary.md) | The project's terms: what each word means, and which words to avoid | People and agents, by hand |

## For coding agents

1. **Read [glossary.md](glossary.md) first.** Use its terms in code, comments, docs and messages, and don't use the words it lists under **Avoid**. When you introduce a new concept, add an entry for it marked *proposed* in the same change. When something is renamed, rename its entry and move the old name to **Avoid**.
2. **Read [../decisions.md](../decisions.md)** for what's been decided and why, so you don't undo choices made on purpose.
3. **Name specs as propositions.** Namespace, class and method together read as one sentence (`A_claim.that_is_false.throws_Expected`). Specs that share the start of a sentence share a namespace or class.
4. **Write specs in Motif.Is.** Claims about values are extensions (`order.Total.Is(11.25m)`); claims about code go through `Check.That(() => …)`. Only the specs marked `// BOOTSTRAP` use plain xUnit `Assert`.

## In this repo

Motif.Is is a library, not an app, so there's no sketch, plan or diagrams here. Motif's sketch and solution plan describe apps (ports, adapters, external systems), and a library has almost none of those. Whether, and how, Motif's tools should cover a library is an open question that this repo is a test case for ([motif-dotnet/.github#24](https://github.com/motif-dotnet/.github/issues/24)).
