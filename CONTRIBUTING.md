# Contributing to PromotionsApp

Thanks for your interest in contributing! This document explains how to propose changes, run tests, and follow conventions so contributions integrate smoothly.

## Getting the code

Clone the repository and open it with your preferred IDE (Visual Studio / VS Code).

## Branching

- Base work on the `main` branch (or the repository default). Create feature branches off the default branch:
  - `feature/your-short-description`
  - `fix/brief-description`
  - `chore/brief-description`

Keep branches focused and small. Rebase or merge the latest default branch before opening a PR.

## Commit messages

- Use concise, imperative messages: `Add NunitsSkuRule unit tests`.
- Include relevant issue number in the message when applicable.

## Pull Requests

- Open a PR with a clear title and description of the change.
- Describe the motivation and what was changed.
- Link any related issues.
- Add reviewers and wait for at least one approval before merging.

## Tests

The project uses `xUnit` for unit tests. Please add tests for new behavior and bug fixes.

Run tests locally with:
```bash
dotnet test PromotionAppTest\PromotionApp.Test.csproj
```

If you add logic to `Promotion.Domain`, include unit tests in `PromotionAppTest` and use `Moq` to mock dependencies (e.g., `IRepository`).

## Coding guidelines

- Follow common C# conventions (PascalCase for types and methods).
- Keep methods small and single-responsibility.
- Prefer interface-driven design for easier testing (`IRepository`, `IRule`).
- Add XML documentation for public-facing APIs when helpful.

## Adding new rules

- Implement the `IRule` interface in `Promotion.Domain/Rules`.
- Ensure the rule respects the `IsActive` property and implements `IsMatch` and `Apply` appropriately.
- Add unit tests covering matching and application logic.

## Reporting issues

- Create a clear issue describing steps to reproduce (if a bug), expected behavior, and actual behavior.
- Include code snippets or failing test output where helpful.

## CI and quality

If CI is added later, ensure your PR passes build and tests before requesting merge.

## Thank you

Thanks for contributing — improvements, bug fixes, and tests are all welcome. If you're unsure where to start, open an issue to discuss your idea.
