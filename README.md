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

## Etsy API Resources

- [Etsy Open API documentation](https://developers.etsy.com/documentation/)
- [Etsy Open API reference](https://developers.etsy.com/documentation/reference)
- [Etsy Open API 3.0.0 specification](https://www.etsy.com/openapi/generated/oas/3.0.0.json)

