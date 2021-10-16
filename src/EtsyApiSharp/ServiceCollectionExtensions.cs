using EtsyApiSharp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EtsyApiSharp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEtsyAuthScoped(this IServiceCollection services, string clientId, string redirectUrl)
        {
            return services.AddScoped<IEtsyAuthService, EtsyAuthService>(_ => new EtsyAuthService(clientId, redirectUrl));
        }
        public static IServiceCollection AddEtsyAuthTransient(this IServiceCollection services, string clientId, string redirectUrl)
        {
            return services.AddTransient<IEtsyAuthService, EtsyAuthService>(_ => new EtsyAuthService(clientId, redirectUrl));
        }
        public static IServiceCollection AddEtsyAuthSingleton(this IServiceCollection services, string clientId, string redirectUrl)
        {
            return services.AddSingleton<IEtsyAuthService, EtsyAuthService>(_ => new EtsyAuthService(clientId, redirectUrl));
        }
    }
}
