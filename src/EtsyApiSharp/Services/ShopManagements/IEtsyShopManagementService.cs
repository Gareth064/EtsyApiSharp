using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;

namespace EtsyApiSharp.Services.ShopManagements;

public interface IEtsyShopManagementService
{
    /// <summary>
    /// Retrieves the shop owned by an Etsy user. This operation does not require OAuth.
    /// </summary>
    Task<ApiResponse<Shop>> GetShopByOwnerUserIdAsync(
        long userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an Etsy shop. This operation does not require OAuth.
    /// </summary>
    Task<ApiResponse<Shop>> GetShopAsync(
        long shopId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an Etsy shop. Requires the <c>shops_r</c> and <c>shops_w</c> OAuth scopes.
    /// </summary>
    Task<ApiResponse<Shop>> UpdateShopAsync(
        string accessToken,
        long shopId,
        UpdateShopRequest update,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds Etsy shops whose names contain the supplied value. This operation does not require OAuth.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<Shop>>> FindShopsAsync(
        string shopName,
        FindShopsByNameFilter? filter = null,
        CancellationToken cancellationToken = default);
}
