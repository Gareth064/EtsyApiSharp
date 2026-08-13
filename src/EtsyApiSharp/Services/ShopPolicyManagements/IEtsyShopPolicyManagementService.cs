using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.ShopPolicies;

namespace EtsyApiSharp.Services.ShopPolicyManagements;

/// <summary>Provides Etsy's Shop Return Policy operations.</summary>
public interface IEtsyShopPolicyManagementService
{
    /// <summary>Consolidates two return policies. Requires the <c>shops_w</c> OAuth scope.</summary>
    Task<ApiResponse<ShopReturnPolicy>> ConsolidateShopReturnPoliciesAsync(string accessToken, long shopId, ConsolidateShopReturnPoliciesRequest request, CancellationToken cancellationToken = default);

    /// <summary>Creates a return policy. Requires the <c>shops_w</c> OAuth scope.</summary>
    Task<ApiResponse<ShopReturnPolicy>> CreateShopReturnPolicyAsync(string accessToken, long shopId, CreateShopReturnPolicyRequest policy, CancellationToken cancellationToken = default);

    /// <summary>Gets a shop's return policies. This operation does not require OAuth.</summary>
    Task<ApiResponse<EtsyListResponse<ShopReturnPolicy>>> GetShopReturnPoliciesAsync(long shopId, CancellationToken cancellationToken = default);

    /// <summary>Deletes a return policy. Requires the <c>shops_w</c> OAuth scope.</summary>
    Task<ApiResponse<object>> DeleteShopReturnPolicyAsync(string accessToken, long shopId, long returnPolicyId, CancellationToken cancellationToken = default);

    /// <summary>Gets a return policy. This operation does not require OAuth.</summary>
    Task<ApiResponse<ShopReturnPolicy>> GetShopReturnPolicyAsync(long shopId, long returnPolicyId, CancellationToken cancellationToken = default);

    /// <summary>Updates a return policy. Requires the <c>shops_w</c> OAuth scope.</summary>
    Task<ApiResponse<ShopReturnPolicy>> UpdateShopReturnPolicyAsync(string accessToken, long shopId, long returnPolicyId, UpdateShopReturnPolicyRequest policy, CancellationToken cancellationToken = default);
}
