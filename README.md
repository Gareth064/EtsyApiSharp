# Etsy Api Sharp
EtsyApiSharp is a wrapper library for the Etsy v3 API

## Features
- Built on the .Net6
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

The library uses OAuth 2.0 for authentication. Access tokens returned from Etsy are in the format `USER_ID.OAUTH_TOKEN` and are automatically prefixed with `Bearer` when making authenticated requests.

For POST/PUT requests with JSON content, the library ensures proper `Content-Type: application/json; charset=utf-8` headers are set as required by Etsy API v3.

## API Compliance

This library follows the [Etsy API v3 Request Standards](https://developer.etsy.com/documentation/essentials/requests/):
- Proper `x-api-key` header format with API key and shared secret
- OAuth 2.0 Bearer token authorization
- UTF-8 charset for JSON content
- Compatible with both `https://api.etsy.com/v3/` and `https://openapi.etsy.com/v3/` endpoints

