using EtsyApiSharp.Models;
using EtsyApiSharp.Services.Auths;
using EtsyApiSharp.Services.ListingManagements;
using EtsyApiSharp.Services.PaymentManagements;
using EtsyApiSharp.Services.ReceiptManagements;
using EtsyApiSharp.Services.ReviewManagements;
using EtsyApiSharp.Services.ShopManagements;
using EtsyApiSharp.Services.ShippingManagements;
using EtsyApiSharp.Services.UserManagements;
using Microsoft.Extensions.DependencyInjection;

namespace EtsyApiSharp;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEtsyAuthServiceScoped(this IServiceCollection services, string clientId, string redirectUrl, IEnumerable<Scope> scopes)
    {
        services.AddHttpClient(EtsyAuthService.HttpClientName);
        return services.AddScoped<IEtsyAuthService, EtsyAuthService>(provider => new EtsyAuthService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, redirectUrl, scopes));
    }

    public static IServiceCollection AddEtsyAuthServiceTransient(this IServiceCollection services, string clientId, string redirectUrl, IEnumerable<Scope> scopes)
    {
        services.AddHttpClient(EtsyAuthService.HttpClientName);
        return services.AddTransient<IEtsyAuthService, EtsyAuthService>(provider => new EtsyAuthService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, redirectUrl, scopes));
    }

    public static IServiceCollection AddEtsyAuthServiceSingleton(this IServiceCollection services, string clientId, string redirectUrl, IEnumerable<Scope> scopes)
    {
        services.AddHttpClient(EtsyAuthService.HttpClientName);
        return services.AddSingleton<IEtsyAuthService, EtsyAuthService>(provider => new EtsyAuthService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, redirectUrl, scopes));
    }



    public static IServiceCollection AddEtsyReceiptManagementServiceScoped(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyReceiptManagementService.HttpClientName);
        return services.AddScoped<IEtsyReceiptManagementService, EtsyReceiptManagementService>(provider => new EtsyReceiptManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }

    public static IServiceCollection AddEtsyReceiptManagementServiceTransient(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyReceiptManagementService.HttpClientName);
        return services.AddTransient<IEtsyReceiptManagementService, EtsyReceiptManagementService>(provider => new EtsyReceiptManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }

    public static IServiceCollection AddEtsyReceiptManagementServiceSingleton(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyReceiptManagementService.HttpClientName);
        return services.AddSingleton<IEtsyReceiptManagementService, EtsyReceiptManagementService>(provider => new EtsyReceiptManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }



    public static IServiceCollection AddEtsyListingManagementServiceScoped(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyListingManagementService.HttpClientName);
        return services.AddScoped<IEtsyListingManagementService, EtsyListingManagementService>(provider => new EtsyListingManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }

    public static IServiceCollection AddEtsyPaymentManagementServiceScoped(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyPaymentManagementService.HttpClientName);
        return services.AddScoped<IEtsyPaymentManagementService, EtsyPaymentManagementService>(provider => new EtsyPaymentManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }

    public static IServiceCollection AddEtsyPaymentManagementServiceTransient(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyPaymentManagementService.HttpClientName);
        return services.AddTransient<IEtsyPaymentManagementService, EtsyPaymentManagementService>(provider => new EtsyPaymentManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }

    public static IServiceCollection AddEtsyPaymentManagementServiceSingleton(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyPaymentManagementService.HttpClientName);
        return services.AddSingleton<IEtsyPaymentManagementService, EtsyPaymentManagementService>(provider => new EtsyPaymentManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }

    public static IServiceCollection AddEtsyReviewManagementServiceScoped(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyReviewManagementService.HttpClientName);
        return services.AddScoped<IEtsyReviewManagementService, EtsyReviewManagementService>(provider => new EtsyReviewManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }

    public static IServiceCollection AddEtsyReviewManagementServiceTransient(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyReviewManagementService.HttpClientName);
        return services.AddTransient<IEtsyReviewManagementService, EtsyReviewManagementService>(provider => new EtsyReviewManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }

    public static IServiceCollection AddEtsyReviewManagementServiceSingleton(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyReviewManagementService.HttpClientName);
        return services.AddSingleton<IEtsyReviewManagementService, EtsyReviewManagementService>(provider => new EtsyReviewManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }

    public static IServiceCollection AddEtsyListingManagementServiceTransient(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyListingManagementService.HttpClientName);
        return services.AddTransient<IEtsyListingManagementService, EtsyListingManagementService>(provider => new EtsyListingManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }

    public static IServiceCollection AddEtsyListingManagementServiceSingleton(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyListingManagementService.HttpClientName);
        return services.AddSingleton<IEtsyListingManagementService, EtsyListingManagementService>(provider => new EtsyListingManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }


    public static IServiceCollection AddEtsyShopManagementServiceScoped(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyShopManagementService.HttpClientName);
        return services.AddScoped<IEtsyShopManagementService, EtsyShopManagementService>(provider => new EtsyShopManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }

    public static IServiceCollection AddEtsyShopManagementServiceTransient(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyShopManagementService.HttpClientName);
        return services.AddTransient<IEtsyShopManagementService, EtsyShopManagementService>(provider => new EtsyShopManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }

    public static IServiceCollection AddEtsyShopManagementServiceSingleton(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyShopManagementService.HttpClientName);
        return services.AddSingleton<IEtsyShopManagementService, EtsyShopManagementService>(provider => new EtsyShopManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }

    public static IServiceCollection AddEtsyShippingManagementServiceScoped(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyShippingManagementService.HttpClientName);
        return services.AddScoped<IEtsyShippingManagementService, EtsyShippingManagementService>(provider => new EtsyShippingManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }

    public static IServiceCollection AddEtsyShippingManagementServiceTransient(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyShippingManagementService.HttpClientName);
        return services.AddTransient<IEtsyShippingManagementService, EtsyShippingManagementService>(provider => new EtsyShippingManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }

    public static IServiceCollection AddEtsyShippingManagementServiceSingleton(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyShippingManagementService.HttpClientName);
        return services.AddSingleton<IEtsyShippingManagementService, EtsyShippingManagementService>(provider => new EtsyShippingManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }


    public static IServiceCollection AddEtsyUserManagementServiceScoped(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyUserManagementService.HttpClientName);
        return services.AddScoped<IEtsyUserManagementService, EtsyUserManagementService>(provider => new EtsyUserManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }

    public static IServiceCollection AddEtsyUserManagementServiceTransient(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyUserManagementService.HttpClientName);
        return services.AddTransient<IEtsyUserManagementService, EtsyUserManagementService>(provider => new EtsyUserManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }

    public static IServiceCollection AddEtsyUserManagementServiceSingleton(this IServiceCollection services, string clientId, string sharedSecret)
    {
        services.AddHttpClient(EtsyUserManagementService.HttpClientName);
        return services.AddSingleton<IEtsyUserManagementService, EtsyUserManagementService>(provider => new EtsyUserManagementService(
            provider.GetRequiredService<IHttpClientFactory>(), clientId, sharedSecret));
    }
}
