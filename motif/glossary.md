# Glossary

The words this project uses, and what they mean. People and agents use these terms in code, docs, issues and discussion, and avoid the words listed under **Avoid**. A new concept gets an entry, marked *proposed*, in the same change that introduces it. An entry with no status is agreed.

The format is described in the [Motif Glossary design note](https://github.com/motif-dotnet/.github/blob/main/design/glossary.md). Motif.Is is a library with no sketch, so no entry has a **Sketch** field, and almost every term is a *domain concept*.

## Bootstrap spec

One of the few specs written with plain xUnit `Assert` rather than Motif.Is, marked `// BOOTSTRAP`. Together they prove that a false claim throws and that `Throws` catches, which every other spec relies on.

- **Kind:** domain concept
- **See also:** [Self-hosted](#self-hosted)

## Claim

What a spec states about a subject: that it is a value, has a count, includes an item, throws an exception. A claim either holds or is false. Claims about values are extensions on the value; claims about code go through `Check.That`.

- **Kind:** domain concept
- **Avoid:** assertion (except when comparing with other libraries), should
- **In code:** `Claims`, `ValueClaims`, `Proposition.Claim`
- **See also:** [Proposition](#proposition), [Subject](#subject)

## Code

Code that a spec makes a claim about, given to `Check.That` as a lambda: `Check.That(() => order.Place())`.

- **Kind:** domain concept
- **Avoid:** action (it's an `Action` in C#, but not in prose)
- **In code:** `Code`, `Check.That`

## Expectation

A claim as it reads after "Expected" in a failure: *to be*, *to have*, *to include*, *to throw*.

- **Kind:** domain concept
- **In code:** `Proposition.Expectation`
- **See also:** [Failure](#failure)

## Expected

The exception thrown when a claim is false. Test runners print its full type name, `Motif.Expected`, before the message, so the name starts the failure's sentence.

- **Kind:** domain concept
- **Avoid:** FalseProposition (its old name)
- **In code:** `Motif.Expected`

## Failure

The message of a false claim: the subject and its expectation, then what was found. ``Motif.Expected : `order.Placed` to be true`` / `but it's false.`

- **Kind:** domain concept
- **Avoid:** error (an error is a spec that breaks, not a claim that's false)
- **In code:** `Proposition.Failure`
- **See also:** [Found](#found)

## Failure gallery

The specs in `Deliberately_failing`, which fail on purpose so their messages can be read in a test runner. Tagged `Kind=FailureGallery`.

- **Kind:** domain concept
- **In code:** `Deliberately_failing`

## Found

What was there instead of what was claimed, as a phrase that follows "but": *it's 7.00*, *it has 2 items: [1, 2]*, *it threw nothing*.

- **Kind:** domain concept
- **Avoid:** actual
- **In code:** `Proposition.Found`

## Holds

Said of a claim that is true.

- **Kind:** domain concept
- **Avoid:** passes (a spec passes; a claim holds)
- **In code:** `Proposition.Holds`

## Motif.Is

This library. Its origin name, Isly, is kept for the story only.

- **Kind:** system
- **Avoid:** Isly (except in the origin story)

## Proposition

A claim kept as structure (subject, claim, expectation, value, whether it holds, and what was found) rather than only as a message, so plain-English specs can be generated from it later.

- **Kind:** domain concept
- **In code:** `Proposition`
- **See also:** [Recording](#recording), [Sentence](#sentence)

## Recording

The propositions a spec states, kept as they're made, per async flow. Started with `Propositions.Record()`.

- **Kind:** domain concept
- **In code:** `Propositions`, `PropositionRecording`

## Released core

The minimal set of claims (`Is`, `IsTrue`, `IsFalse` and `Throws`), released as a package that Motif.Is's own specs are checked with, as a compiler is built by its previous release. Alpha 1 is the first.

- **Kind:** domain concept
- **See also:** [Bootstrap spec](#bootstrap-spec), [Self-hosted](#self-hosted)

## Self-hosted

Said of Motif.Is's own specs: they're written in Motif.Is, apart from the bootstrap specs.

- **Kind:** domain concept
- **See also:** [Bootstrap spec](#bootstrap-spec)

## Sentence

A claim as a statement of fact: `` `order.Total` is 11.25 ``. The form plain-English specs would use.

- **Kind:** domain concept
- **In code:** `Proposition.Sentence`

## Spec

An automated check of what the code does, named as a proposition across namespace, class and method.

- **Kind:** domain concept
- **Avoid:** test (except for test runners and frameworks), unit test
- **See also:** [Claim](#claim)

## Subject

What a claim is about, as written in the spec: `order.Total`, `order.Place()`. Taken from the source text by `CallerArgumentExpression`.

- **Kind:** domain concept
- **In code:** `Proposition.Subject`

## Value

The claim's object, shown as text: the 11.25 in "is 11.25". A claim like *is null* has none.

- **Kind:** domain concept
- **Avoid:** expected value
- **In code:** `Proposition.Value`
