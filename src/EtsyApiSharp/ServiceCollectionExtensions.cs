using EtsyApiSharp.Models;
using EtsyApiSharp.Services.Auths;
using EtsyApiSharp.Services.ListingManagements;
using EtsyApiSharp.Services.ReceiptManagements;
using EtsyApiSharp.Services.ShopManagements;
using EtsyApiSharp.Services.UserManagements;
using Microsoft.Extensions.DependencyInjection;

namespace EtsyApiSharp;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEtsyAuthServiceScoped(this IServiceCollection services, string clientId, string redirectUrl, List<Scope> scopes) =>
        services.AddScoped<IEtsyAuthService, EtsyAuthService>(_ => new EtsyAuthService(clientId, redirectUrl, scopes));

    public static IServiceCollection AddEtsyAuthServiceTransient(this IServiceCollection services, string clientId, string redirectUrl, List<Scope> scopes) =>
        services.AddTransient<IEtsyAuthService, EtsyAuthService>(_ => new EtsyAuthService(clientId, redirectUrl, scopes));

    public static IServiceCollection AddEtsyAuthServiceSingleton(this IServiceCollection services, string clientId, string redirectUrl, List<Scope> scopes) =>
        services.AddSingleton<IEtsyAuthService, EtsyAuthService>(_ => new EtsyAuthService(clientId, redirectUrl, scopes));



    public static IServiceCollection AddEtsyReceiptManagementServiceScoped(this IServiceCollection services, string clientId, string sharedSecret) =>
services.AddScoped<IEtsyReceiptManagementService, EtsyReceiptManagementService>(_ => new EtsyReceiptManagementService(clientId, sharedSecret));

    public static IServiceCollection AddEtsyReceiptManagementServiceTransient(this IServiceCollection services, string clientId, string sharedSecret) =>
services.AddTransient<IEtsyReceiptManagementService, EtsyReceiptManagementService>(_ => new EtsyReceiptManagementService(clientId, sharedSecret));

    public static IServiceCollection AddEtsyReceiptManagementServiceSingleton(this IServiceCollection services, string clientId, string sharedSecret) =>
services.AddSingleton<IEtsyReceiptManagementService, EtsyReceiptManagementService>(_ => new EtsyReceiptManagementService(clientId, sharedSecret));



    public static IServiceCollection AddEtsyListingManagementServiceScoped(this IServiceCollection services, string clientId, string sharedSecret) =>
services.AddScoped<IEtsyListingManagementService, EtsyListingManagementService>(_ => new EtsyListingManagementService(clientId, sharedSecret));

    public static IServiceCollection AddEtsyListingManagementServiceTransient(this IServiceCollection services, string clientId, string sharedSecret) =>
services.AddTransient<IEtsyListingManagementService, EtsyListingManagementService>(_ => new EtsyListingManagementService(clientId, sharedSecret));

    public static IServiceCollection AddEtsyListingManagementServiceSingleton(this IServiceCollection services, string clientId, string sharedSecret) =>
services.AddSingleton<IEtsyListingManagementService, EtsyListingManagementService>(_ => new EtsyListingManagementService(clientId, sharedSecret));


    public static IServiceCollection AddEtsyShopManagementServiceScoped(this IServiceCollection services, string clientId, string sharedSecret) =>
services.AddScoped<IEtsyShopManagementService, EtsyShopManagementService>(_ => new EtsyShopManagementService(clientId, sharedSecret));

    public static IServiceCollection AddEtsyShopManagementServiceTransient(this IServiceCollection services, string clientId, string sharedSecret) =>
services.AddTransient<IEtsyShopManagementService, EtsyShopManagementService>(_ => new EtsyShopManagementService(clientId, sharedSecret));

    public static IServiceCollection AddEtsyShopManagementServiceSingleton(this IServiceCollection services, string clientId, string sharedSecret) =>
services.AddSingleton<IEtsyShopManagementService, EtsyShopManagementService>(_ => new EtsyShopManagementService(clientId, sharedSecret));


    public static IServiceCollection AddEtsyUserManagementServiceScoped(this IServiceCollection services, string clientId, string sharedSecret) =>
services.AddScoped<IEtsyUserManagementService, EtsyUserManagementService>(_ => new EtsyUserManagementService(clientId, sharedSecret));

    public static IServiceCollection AddEtsyUserManagementServiceTransient(this IServiceCollection services, string clientId, string sharedSecret) =>
services.AddTransient<IEtsyUserManagementService, EtsyUserManagementService>(_ => new EtsyUserManagementService(clientId, sharedSecret));

    public static IServiceCollection AddEtsyUserManagementServiceSingleton(this IServiceCollection services, string clientId, string sharedSecret) =>
services.AddSingleton<IEtsyUserManagementService, EtsyUserManagementService>(_ => new EtsyUserManagementService(clientId, sharedSecret));
}
