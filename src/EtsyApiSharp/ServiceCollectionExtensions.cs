using EtsyApiSharp.Models;
using EtsyApiSharp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EtsyApiSharp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEtsyAuthScoped(this IServiceCollection services, string clientId, string redirectUrl, List<Scope> scopes) => 
            services.AddScoped<IEtsyAuthService, EtsyAuthService>(_ => new EtsyAuthService(clientId, redirectUrl, scopes));
        
        public static IServiceCollection AddEtsyAuthTransient(this IServiceCollection services, string clientId, string redirectUrl, List<Scope> scopes) =>
            services.AddTransient<IEtsyAuthService, EtsyAuthService>(_ => new EtsyAuthService(clientId, redirectUrl, scopes));
        
        public static IServiceCollection AddEtsyAuthSingleton(this IServiceCollection services, string clientId, string redirectUrl, List<Scope> scopes) =>
            services.AddSingleton<IEtsyAuthService, EtsyAuthService>(_ => new EtsyAuthService(clientId, redirectUrl, scopes));
        
    }
}
