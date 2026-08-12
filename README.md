# Etsy Api Sharp
EtsyApiSharp is a wrapper library for the Etsy v3 API

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
services.AddEtsyListingManagementServiceScoped(clientId, sharedSecret);
services.AddEtsyReceiptManagementServiceScoped(clientId, sharedSecret);
services.AddEtsyPaymentManagementServiceScoped(clientId, sharedSecret);
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

## Etsy API Resources

- [Etsy Open API documentation](https://developers.etsy.com/documentation/)
- [Etsy Open API reference](https://developers.etsy.com/documentation/reference)
- [Etsy Open API 3.0.0 specification](https://www.etsy.com/openapi/generated/oas/3.0.0.json)

