using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;

namespace EtsyApiSharp.Services.ShopEconomicOperatorManagements;

/// <summary>
/// Provides Etsy Shop Economic Operator operations.
/// </summary>
public interface IEtsyShopEconomicOperatorManagementService
{
    /// <summary>Creates an economic operator. Requires the <c>listings_w</c> OAuth scope.</summary>
    Task<ApiResponse<ShopEconomicOperator>> CreateShopEconomicOperatorAsync(string accessToken, long shopId, CreateShopEconomicOperatorRequest economicOperator, CancellationToken cancellationToken = default);

    /// <summary>Gets a shop's economic operators. Requires the <c>listings_r</c> OAuth scope.</summary>
    Task<ApiResponse<EtsyListResponse<ShopEconomicOperator>>> GetShopEconomicOperatorsAsync(string accessToken, long shopId, CancellationToken cancellationToken = default);

    /// <summary>Gets an economic operator. Requires the <c>listings_r</c> OAuth scope.</summary>
    Task<ApiResponse<ShopEconomicOperator>> GetShopEconomicOperatorAsync(string accessToken, long shopId, long economicOperatorId, CancellationToken cancellationToken = default);

    /// <summary>Updates an economic operator. Requires the <c>listings_w</c> OAuth scope.</summary>
    Task<ApiResponse<ShopEconomicOperator>> UpdateShopEconomicOperatorAsync(string accessToken, long shopId, long economicOperatorId, UpdateShopEconomicOperatorRequest economicOperator, CancellationToken cancellationToken = default);
}
