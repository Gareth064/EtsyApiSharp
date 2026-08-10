# Repository Guidelines

## Project Structure & Module Organization

The solution lives at `src/EtsyApiSharp.sln` and targets .NET 6. Production code is in `src/EtsyApiSharp/`: API services are grouped by Etsy domain under `Services/`, response and request DTOs under `Models/`, and shared HTTP, authentication, conversion, and extension code under `Helpers/` and `Infrastructure/`. `ServiceCollectionExtensions.cs` is the public dependency-injection entry point. `src/BlazorTestApp/` is a manual integration/demo application; its Razor pages are under `Pages/` and static assets under `wwwroot/`.

There is currently no automated test project. Add one beside the existing projects (for example, `src/EtsyApiSharp.Tests/`) and include it in the solution.

## Build, Test, and Development Commands

Run commands from the repository root:

- `dotnet restore src/EtsyApiSharp.sln` — restore NuGet packages.
- `dotnet build src/EtsyApiSharp.sln` — compile the library and demo app.
- `dotnet test src/EtsyApiSharp.sln` — run all test projects once tests exist.
- `dotnet run --project src/BlazorTestApp/BlazorTestApp.csproj` — launch the Blazor test harness (normally at `https://localhost:5001`).
- `dotnet format src/EtsyApiSharp.sln --verify-no-changes` — check formatting before submitting changes.

## Coding Style & Naming Conventions

Follow standard C# conventions: four-space indentation, file-scoped namespaces, `PascalCase` for public members and types, and `camelCase` for parameters and private fields. Keep one primary type per file. Name service contracts `IEtsy...Service` and implementations `Etsy...Service`; suffix asynchronous methods with `Async`. Nullable reference types and implicit usings are enabled. The repository's `src/.editorconfig` suppresses CS8618 only; do not broadly suppress new warnings.

## Testing Guidelines

No test framework or coverage threshold is configured. For new behavior, add focused unit tests (prefer xUnit) named `MethodName_Scenario_ExpectedResult`. Cover URL/query construction, JSON converters, response parsing, and error paths. Mock HTTP boundaries; do not make routine tests depend on live Etsy accounts. Run `dotnet test` before opening a pull request.

## Commit & Pull Request Guidelines

Recent commits use short, imperative summaries such as `Add HttpContentHelper...` and `Update BlazorTestApp...`. Keep each commit focused and explain the user-visible outcome. Pull requests should include a concise problem/solution description, linked issue when applicable, test evidence, and any API compatibility impact. Include screenshots only for Blazor UI changes.

## Security & Configuration

Never commit Etsy client IDs, shared secrets, OAuth tokens, or authorization codes. Supply `EtsyConfig:ClientId` and `EtsyConfig:SharedSecret` through user secrets or environment variables (for example, `EtsyConfig__ClientId`). Use disposable test credentials for manual integration checks.
