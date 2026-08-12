# Repository Guidelines

## Project Structure & Module Organization

The solution lives at `src/EtsyApiSharp.sln` and targets .NET 10. Production code is in `src/EtsyApiSharp/`: API services are grouped by Etsy domain under `Services/`, response and request DTOs under `Models/`, and shared HTTP, authentication, conversion, and extension code under `Helpers/` and `Infrastructure/`. `ServiceCollectionExtensions.cs` is the public dependency-injection entry point. `src/BlazorTestApp/` is a manual integration/demo application; its Razor pages are under `Pages/` and static assets under `wwwroot/`.

The xUnit test project is at `src/EtsyApiSharp.Tests/` and is included in the solution.

## Etsy API Resources

Use these official Etsy resources when implementing or reviewing API services:

- [Etsy Open API documentation](https://developers.etsy.com/documentation/)
- [Etsy Open API reference](https://developers.etsy.com/documentation/reference)
- [Etsy Open API 3.0.0 specification](https://www.etsy.com/openapi/generated/oas/3.0.0.json)

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

The test project uses xUnit. Add focused unit tests named `MethodName_Scenario_ExpectedResult`. Cover URL/query construction, JSON converters, response parsing, and error paths. Mock HTTP boundaries; do not make routine tests depend on live Etsy accounts. Run `dotnet test` before opening a pull request.

## Commit & Pull Request Guidelines

Recent commits use short, imperative summaries such as `Add HttpContentHelper...` and `Update BlazorTestApp...`. Keep each commit focused and explain the user-visible outcome. Pull requests should include a concise problem/solution description, linked issue when applicable, test evidence, and any API compatibility impact. Include screenshots only for Blazor UI changes.

## Security & Configuration

Never commit Etsy client IDs, shared secrets, OAuth tokens, or authorization codes. Supply `EtsyConfig:ClientId` and `EtsyConfig:SharedSecret` through user secrets or environment variables (for example, `EtsyConfig__ClientId`). Use disposable test credentials for manual integration checks.

## Listing API maintenance

Treat the Etsy OpenAPI 3.0.0 specification as the source of truth for all Listing Management routes, request parameters, response types, and OAuth scopes. Maintain the complete 46-operation surface across listing lifecycle, files, images, inventory, offerings, products, personalization, translations, variation images, videos, and both taxonomies. Public operations use the `x-api-key` header; add a bearer token only when the operation declares an OAuth scope in the specification. Keep the service on its named `IHttpClientFactory` client, validate IDs and pagination before issuing a request, encode query parameters safely, and expose cancellation through every asynchronous method.

When listing-service behavior changes, update `src/BlazorTestApp/Pages/Services/Listing.razor`. Mock HTTP boundaries in tests and cover the changed route, query/authentication behavior, response parsing, invalid input, cancellation, and DI registration.

## Receipt API maintenance

Treat the Etsy OpenAPI 3.0.0 specification as the source of truth for all Receipt Management routes, request parameters, form/JSON payloads, response types, and OAuth scopes. Maintain the complete 8-operation surface across receipts, receipt state updates, shipment tracking, and receipt transactions. Every Receipt Management operation currently requires `x-api-key` and OAuth: reads require `transactions_r`, while receipt updates and shipment creation require `transactions_w`. Keep the service on its named `IHttpClientFactory` client, validate IDs and pagination before issuing a request, encode query parameters safely, and expose cancellation through every asynchronous method.

When receipt-service behavior changes, update `src/BlazorTestApp/Pages/Services/Receipt.razor` for non-destructive reads. Mock HTTP boundaries in tests and cover each route, method, query or payload serialization, authentication behavior, response parsing, invalid input, cancellation, and DI registration.

## Shop API maintenance

Treat the Etsy OpenAPI 3.0.0 specification as the source of truth for all Shop Management routes, request parameters, form payloads, response types, and OAuth scopes. Maintain the complete 10-operation surface across shop lookup/search/update, production partners, and shop sections. Public shop and section reads use `x-api-key` only; production partners require `shops_r`; shop updates require `shops_r` and `shops_w`; shop-section writes require `shops_w`. Keep the service on its named `IHttpClientFactory` client, validate IDs and pagination before issuing a request, encode query parameters safely, and expose cancellation through every asynchronous method.

When shop-service behavior changes, update `src/BlazorTestApp/Pages/Services/Shop.razor` for all non-destructive reads. Mock HTTP boundaries in tests and cover each route, method, query or form serialization, authentication behavior, response parsing, invalid input, cancellation, and DI registration.
