using EtsyApiSharp.Models;
using EtsyApiSharp.Services.Auths;
using EtsyApiSharp.Services.ListingManagements;
using EtsyApiSharp.Services.ReceiptManagements;
using Microsoft.Extensions.DependencyInjection;

namespace EtsyApiSharp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEtsyAuthServiceScoped(this IServiceCollection services, string clientId, string redirectUrl, List<Scope> scopes) =>
            services.AddScoped<IEtsyAuthService, EtsyAuthService>(_ => new EtsyAuthService(clientId, redirectUrl, scopes));

        public static IServiceCollection AddEtsyAuthServiceTransient(this IServiceCollection services, string clientId, string redirectUrl, List<Scope> scopes) =>
            services.AddTransient<IEtsyAuthService, EtsyAuthService>(_ => new EtsyAuthService(clientId, redirectUrl, scopes));

        public static IServiceCollection AddEtsyAuthServiceSingleton(this IServiceCollection services, string clientId, string redirectUrl, List<Scope> scopes) =>
            services.AddSingleton<IEtsyAuthService, EtsyAuthService>(_ => new EtsyAuthService(clientId, redirectUrl, scopes));



        public static IServiceCollection AddEtsyReceiptManagementServiceScoped(this IServiceCollection services, string clientId) =>
    services.AddScoped<IEtsyReceiptManagementService, EtsyReceiptManagementService>(_ => new EtsyReceiptManagementService(clientId));

        public static IServiceCollection AddEtsyReceiptManagementServiceTransient(this IServiceCollection services, string clientId) =>
    services.AddTransient<IEtsyReceiptManagementService, EtsyReceiptManagementService>(_ => new EtsyReceiptManagementService(clientId));

        public static IServiceCollection AddEtsyReceiptManagementServiceSingleton(this IServiceCollection services, string clientId) =>
    services.AddSingleton<IEtsyReceiptManagementService, EtsyReceiptManagementService>(_ => new EtsyReceiptManagementService(clientId));



        public static IServiceCollection AddEtsyListingManagementServiceScoped(this IServiceCollection services, string clientId) =>
services.AddScoped<IEtsyListingManagementService, EtsyListingManagementService>(_ => new EtsyListingManagementService(clientId));

        public static IServiceCollection AddEtsyListingManagementServiceTransient(this IServiceCollection services, string clientId) =>
    services.AddTransient<IEtsyListingManagementService, EtsyListingManagementService>(_ => new EtsyListingManagementService(clientId));

        public static IServiceCollection AddEtsyListingManagementServiceSingleton(this IServiceCollection services, string clientId) =>
    services.AddSingleton<IEtsyListingManagementService, EtsyListingManagementService>(_ => new EtsyListingManagementService(clientId));
    }
}
