# Etsy Api Sharp
EtsyApiSharp is a wrapper library for the Etsy v3 API

## Installation

Install the package from NuGet:

```shell
dotnet add package EtsyApiSharp
```

The package supports .NET 10. For an application that uses dependency injection, register only the Etsy services it needs as shown below.

## Features
- Built on .NET 10
- Services can be added individually and at different scopes in the DI framework. (Only register what you need)
- Simple and easy to use
- Fully compliant with Etsy API v3 request standards

## Configuration

To use the library, you need to provide both your API Key (Client ID) and Shared Secret from your Etsy app. These are combined in the `x-api-key` header format as required by Etsy API v3 documentation: `API_KEY:SHARED_SECRET`.

### Service Registration

Register services in your dependency injection container:

```csharp
// Register Auth Service
services.AddEtsyAuthServiceScoped(clientId, redirectUrl, scopes);

// Register Management Services
services.AddEtsyShopManagementServiceScoped(clientId, sharedSecret);
services.AddEtsyShopPolicyManagementServiceScoped(clientId, sharedSecret);
services.AddEtsyShippingManagementServiceScoped(clientId, sharedSecret);
services.AddEtsyListingManagementServiceScoped(clientId, sharedSecret);
services.AddEtsyReceiptManagementServiceScoped(clientId, sharedSecret);
services.AddEtsyPaymentManagementServiceScoped(clientId, sharedSecret);
services.AddEtsyReviewManagementServiceScoped(clientId, sharedSecret);
services.AddEtsyUserManagementServiceScoped(clientId, sharedSecret);
```

### Authentication

The library uses Etsy's OAuth 2.0 authorization-code flow with PKCE. Generate a fresh code verifier and state value for each authorization request, retain both for the callback, and reject a callback whose state does not match:

```csharp
var codeVerifier = AuthHelper.CreateCodeVerifier();
var state = AuthHelper.CreateState();
var authorizationUrl = authService.BuildAuthorizationUrl(codeVerifier, state);

// After Etsy redirects to the registered HTTPS callback URL:
if (callbackState != state)
    throw new InvalidOperationException("OAuth state mismatch.");

var token = await authService.GetFirstAccessTokenAsync(
    authorizationCode,
    codeVerifier,
    cancellationToken);
```

The auth service uses the named `IHttpClientFactory` client `EtsyAuthService.HttpClientName`, allowing consuming applications to configure timeouts, handlers, logging, and resilience policies through the standard .NET HTTP client pipeline. Access tokens returned from Etsy are in the format `USER_ID.OAUTH_TOKEN`.

For POST/PUT requests with JSON content, the library ensures proper `Content-Type: application/json; charset=utf-8` headers are set as required by Etsy API v3.

## API Compliance

This library follows the [Etsy API v3 Request Standards](https://developers.etsy.com/documentation/essentials/requests/):
- Proper `x-api-key` header format with API key and shared secret
- OAuth 2.0 Bearer token authorization
- UTF-8 charset for JSON content
- Compatible with both `https://api.etsy.com/v3/` and `https://openapi.etsy.com/v3/` endpoints

### Listing service

`IEtsyListingManagementService` implements all 46 operations currently grouped as Listing Management in Etsy's OpenAPI 3.0.0 specification: listing and taxonomy queries, listing lifecycle and properties, files, images, inventory, offerings, products, personalization, translations, variation images, and videos.

Every method validates positive Etsy IDs, validates pagination (`limit` 1–100 and a non-negative `offset`), accepts a `CancellationToken`, and uses the named `EtsyListingManagementService.HttpClientName` client. The service applies Etsy's operation-level authentication: public reads send only `x-api-key`; protected reads and writes require the access token and scope declared by Etsy (`listings_r`, `listings_w`, `listings_d`, `transactions_r`, or `shops_r`).

```csharp
var listing = await listingService.GetListingAsync(
    listingId,
    new[] { ListingInclude.Images, ListingInclude.Inventory },
    language: "en",
    cancellationToken: cancellationToken);

var shopListings = await listingService.GetListingsByShopAsync(
    accessToken,
    shopId,
    filter: new GetListingsByShopFilter { State = ListingState.active },
    cancellationToken: cancellationToken);

var draft = await listingService.CreateDraftListingAsync(
    accessToken,
    shopId,
    new CreateDraftListingRequest
    {
        Quantity = 1,
        Title = "Example listing",
        Description = "Example description",
        Price = 10.00m,
        WhoMade = ListingWhoMade.i_did,
        WhenMade = ListingWhenMadeConstant._MadeToOrder,
        TaxonomyId = taxonomyId
    },
    cancellationToken);
```

The listing API was updated to remove unnecessary OAuth-token parameters from public endpoints. Update existing callers to supply a token only where the corresponding Etsy operation requires one. Upload APIs use `Stream`-based multipart requests; callers own the source stream and should not reuse it after the request completes.

### Shop service

`IEtsyShopManagementService` implements all 10 operations currently grouped as Shop Management in Etsy's OpenAPI 3.0.0 specification: shop lookup and search, shop updates, production partners, and shop-section creation, retrieval, update, and deletion.

Public reads (`GetShopAsync`, `GetShopByOwnerUserIdAsync`, `FindShopsAsync`, `GetShopSectionsAsync`, and `GetShopSectionAsync`) send only `x-api-key`. `GetShopProductionPartnersAsync` requires `shops_r`; `UpdateShopAsync` requires both `shops_r` and `shops_w`; section creation, update, and deletion require `shops_w`. The write methods use `application/x-www-form-urlencoded` bodies. All methods validate positive IDs, accept a `CancellationToken`, and use the named `EtsyShopManagementService.HttpClientName` client.

```csharp
var sections = await shopService.GetShopSectionsAsync(shopId, cancellationToken);

var section = await shopService.CreateShopSectionAsync(
    accessToken,
    shopId,
    new CreateShopSectionRequest { Title = "Seasonal" },
    cancellationToken);
```

### Shop Policy service

`IEtsyShopPolicyManagementService` implements all 6 operations currently grouped as Shop Policy Management (the `Shop Return Policy` tag) in Etsy's OpenAPI 3.0.0 specification: return-policy consolidation, creation, collection and single-policy retrieval, update, and deletion.

`GetShopReturnPoliciesAsync` and `GetShopReturnPolicyAsync` are public reads and send only `x-api-key`. Consolidation, creation, update, and deletion require `x-api-key` plus an OAuth access token with `shops_w`. Write operations use `application/x-www-form-urlencoded` bodies. Every method validates positive IDs, validates the documented return-deadline values (7, 14, 21, 30, 45, 60, or 90 days), accepts a `CancellationToken`, and uses the named `EtsyShopPolicyManagementService.HttpClientName` client.

```csharp
var policies = await shopPolicyService.GetShopReturnPoliciesAsync(shopId, cancellationToken);

var policy = await shopPolicyService.CreateShopReturnPolicyAsync(
    accessToken,
    shopId,
    new CreateShopReturnPolicyRequest
    {
        AcceptsReturns = true,
        AcceptsExchanges = false,
        ReturnDeadline = 30
    },
    cancellationToken);
```

**Compatibility:** Shop Policy Management is newly available as a separate typed service. Existing Shop Management and Listing Management APIs are unchanged; `GetListingsByShopReturnPolicyAsync` remains part of Listing Management because Etsy groups that operation there.

### Shipping service

`IEtsyShippingManagementService` implements all 14 operations currently grouped as Shipping Management (the `Shop ShippingProfile` tag) in Etsy's OpenAPI 3.0.0 specification: shipping-carrier lookup; shipping-profile creation, retrieval, update, and deletion; and the equivalent destination and upgrade operations.

`GetShippingCarriersAsync` is a public read and sends only `x-api-key`. All shipping-profile, destination, and upgrade reads require `shops_r`; their writes require `shops_w`. Write operations use `application/x-www-form-urlencoded` bodies. Every method validates IDs and documented request relationships, accepts a `CancellationToken`, safely encodes the destination-pagination query, and uses the named `EtsyShippingManagementService.HttpClientName` client.

```csharp
var carriers = await shippingService.GetShippingCarriersAsync("GB", cancellationToken);

var profiles = await shippingService.GetShopShippingProfilesAsync(
    accessToken,
    shopId,
    cancellationToken);

var profile = await shippingService.CreateShopShippingProfileAsync(
    accessToken,
    shopId,
    new CreateShopShippingProfileRequest
    {
        Title = "Standard shipping",
        OriginCountryIso = "GB",
        PrimaryCost = 3.50f,
        SecondaryCost = 1.00f,
        DestinationCountryIso = "US",
        MinDeliveryDays = 5,
        MaxDeliveryDays = 10
    },
    cancellationToken);
```

**Compatibility:** Shipping Management is newly exposed as its own typed service. The existing `ShopShippingProfile`, destination, upgrade, and carrier response models are now paired with create/update request models and typed `shops_r`/`shops_w` operations.

### Receipt service

`IEtsyReceiptManagementService` implements all 8 operations currently grouped as Receipt Management in Etsy's OpenAPI 3.0.0 specification: receipt retrieval and filtering, receipt status updates, shipment tracking creation, and receipt transactions by listing, receipt, transaction, or shop.

All receipt operations require both the `x-api-key` header and an OAuth access token. Read operations require `transactions_r`; `UpdateShopReceiptAsync` and `CreateReceiptShipmentAsync` require `transactions_w`. Receipt updates are sent as `application/x-www-form-urlencoded`; shipment tracking is sent as JSON. Every operation validates positive IDs, validates pagination where Etsy supports it, accepts a `CancellationToken`, and uses the named `EtsyReceiptManagementService.HttpClientName` client.

```csharp
var receipts = await receiptService.GetShopReceiptsAsync(
    accessToken,
    shopId,
    new GetShopReceiptsFilter { WasPaid = true, Limit = 25 },
    cancellationToken);

var shipment = await receiptService.CreateReceiptShipmentAsync(
    accessToken,
    shopId,
    receiptId,
    new CreateReceiptShipmentRequest { TrackingCode = "TRACKING-CODE", CarrierName = "Carrier" },
    cancellationToken: cancellationToken);
```

`TransactionVariation.QuestionId` and `ShopRefund.CreatedTimestamp` now reflect the current response schema (`question_id` and an `int64` timestamp respectively). `ShopRefund` reason, issuer note, and status are nullable because Etsy may omit them.

### Payment service

`IEtsyPaymentManagementService` implements all 5 Payment Management operations currently listed in Etsy's OpenAPI 3.0.0 specification: single and ranged payment-account ledger entries, payments for ledger entries, a receipt's payment, and shop payments by ID.

Every Payment Management operation is a non-destructive read that requires both `x-api-key` and an OAuth access token with `transactions_r`. The service validates positive IDs, the required ledger-entry timestamp range (Unix timestamps on or after 2000-01-01 UTC), and pagination (`limit` 1–100 and a non-negative `offset`). It uses `EtsyPaymentManagementService.HttpClientName` and safely encodes comma-separated ID queries. `Payment`, `PaymentAccountLedgerEntry`, and `PaymentAdjustment` now expose the current int64 timestamps and newly documented payment-adjustment and ledger fields. **Compatibility:** several previously `int` public properties are now `long` (and nullable fields are now nullable), so callers that explicitly assign these values to narrower non-nullable types must update.

```csharp
var ledgerEntries = await paymentService.GetShopPaymentAccountLedgerEntriesAsync(
    accessToken,
    shopId,
    new GetShopPaymentAccountLedgerEntriesFilter
    {
        MinCreated = DateTimeOffset.UtcNow.AddDays(-30).ToUnixTimeSeconds(),
        MaxCreated = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
        Limit = 25
    },
    cancellationToken);

var payments = await paymentService.GetPaymentsAsync(
    accessToken,
    shopId,
    new long[] { paymentId },
    cancellationToken);
```

### Review service

`IEtsyReviewManagementService` implements both operations currently grouped as Review Management in Etsy's OpenAPI 3.0.0 specification: reviews by listing and reviews by shop.

Both operations are public, non-destructive reads that send only `x-api-key`; Etsy declares no OAuth scope for either one. They validate positive IDs, pagination (`limit` 1–100 and a non-negative `offset`), and supplied review creation timestamps (Unix timestamps on or after 2000-01-01 UTC). The service uses `EtsyReviewManagementService.HttpClientName` and accepts a `CancellationToken` on every method.

```csharp
var listingReviews = await reviewService.GetReviewsByListingAsync(
    listingId,
    new GetReviewsFilter
    {
        MinCreated = DateTimeOffset.UtcNow.AddDays(-30).ToUnixTimeSeconds(),
        Limit = 25
    },
    cancellationToken);

var shopReviews = await reviewService.GetReviewsByShopAsync(shopId, cancellationToken: cancellationToken);
```

**Compatibility:** `EtsyListResponse<T>.Count`, review timestamps, and review ratings now use `long`, matching Etsy's documented `int64` response fields. `ListingReview.Review` and review image URLs are nullable because Etsy may omit them; `TransactionReview` now also exposes the documented `created_timestamp` and `updated_timestamp` fields.

### User service

`IEtsyUserManagementService` implements all 5 operations currently grouped as User Management in Etsy's OpenAPI 3.0.0 specification: user-profile and authenticated user/shop-ID lookups, address collection and single-address retrieval, and address deletion.

Every operation requires `x-api-key` and OAuth. `GetUserAsync` requires `email_r` and Etsy limits profiles to the authenticated user or linked buyers; `GetMeAsync` requires `shops_r`; and the three address operations require `address_r` (including deletion, as currently declared by Etsy's specification). Address-list retrieval validates pagination (`limit` 1–100 and non-negative `offset`); all methods validate IDs where applicable, reject missing access tokens, accept a `CancellationToken`, and use the named `EtsyUserManagementService.HttpClientName` client. There are no public User Management operations.

```csharp
var me = await userService.GetMeAsync(accessToken, cancellationToken);

var user = await userService.GetUserAsync(
    accessToken,
    me.Data!.UserId,
    cancellationToken);

var addresses = await userService.GetUserAddressesAsync(accessToken, cancellationToken: cancellationToken);
```

**Compatibility:** three User Address endpoints are newly available. `UserAddress` location fields that Etsy may omit are now nullable; `User.PrimaryEmail` remains nullable because Etsy returns it only for integrations granted field-level access.

## Etsy API Resources

- [Etsy Open API documentation](https://developers.etsy.com/documentation/)
- [Etsy Open API reference](https://developers.etsy.com/documentation/reference)
- [Etsy Open API 3.0.0 specification](https://www.etsy.com/openapi/generated/oas/3.0.0.json)

## Publishing a release

The project is configured to produce both a `.nupkg` and a `.snupkg` symbol package. To build a release package locally, run:

```shell
dotnet pack src/EtsyApiSharp/EtsyApiSharp.csproj --configuration Release -p:Version=1.0.0 --output ./artifacts
```

After reviewing the artifacts, publish them with a NuGet API key. In PowerShell:

```powershell
dotnet nuget push ./artifacts/EtsyApiSharp.1.0.0.nupkg --api-key $env:NUGET_API_KEY --source https://api.nuget.org/v3/index.json
dotnet nuget push ./artifacts/EtsyApiSharp.1.0.0.snupkg --api-key $env:NUGET_API_KEY --source https://api.nuget.org/v3/index.json
```

GitHub releases also publish automatically through the `Publish NuGet package` workflow. Store a NuGet.org API key in the repository's `NUGET_API_KEY` Actions secret, then create a release tagged `v<version>` (for example, `v1.0.0`).

