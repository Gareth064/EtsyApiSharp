using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;

namespace EtsyApiSharp.Services.ShopManagements;
/// <summary>
/// Represents I Etsy Shop Management Service.
/// </summary>

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

    /// <summary>Retrieves a shop's production partners. Requires the <c>shops_r</c> OAuth scope.</summary>
    Task<ApiResponse<EtsyListResponse<ShopProductionPartner>>> GetShopProductionPartnersAsync(
        string accessToken,
        long shopId,
        CancellationToken cancellationToken = default);

    /// <summary>Creates a shop section. Requires the <c>shops_w</c> OAuth scope.</summary>
    Task<ApiResponse<ShopSection>> CreateShopSectionAsync(
        string accessToken,
        long shopId,
        CreateShopSectionRequest section,
        CancellationToken cancellationToken = default);

    /// <summary>Retrieves a shop's sections. This operation does not require OAuth.</summary>
    Task<ApiResponse<EtsyListResponse<ShopSection>>> GetShopSectionsAsync(
        long shopId,
        CancellationToken cancellationToken = default);

    /// <summary>Deletes a shop section. Requires the <c>shops_w</c> OAuth scope.</summary>
    Task<ApiResponse<object>> DeleteShopSectionAsync(
        string accessToken,
        long shopId,
        long shopSectionId,
        CancellationToken cancellationToken = default);

    /// <summary>Retrieves a shop section. This operation does not require OAuth.</summary>
    Task<ApiResponse<ShopSection>> GetShopSectionAsync(
        long shopId,
        long shopSectionId,
        CancellationToken cancellationToken = default);

    /// <summary>Updates a shop section. Requires the <c>shops_w</c> OAuth scope.</summary>
    Task<ApiResponse<ShopSection>> UpdateShopSectionAsync(
        string accessToken,
        long shopId,
        long shopSectionId,
        UpdateShopSectionRequest section,
        CancellationToken cancellationToken = default);
}
