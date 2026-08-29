using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;

namespace EtsyApiSharp.Services.ShippingManagements;

/// <summary>Provides Etsy's Shop ShippingProfile operations.</summary>
public interface IEtsyShippingManagementService
{
    /// <summary>Gets carriers for an origin country. This operation does not require OAuth.</summary>
    Task<ApiResponse<EtsyListResponse<ShippingCarrier>>> GetShippingCarriersAsync(string originCountryIso, CancellationToken cancellationToken = default);

    /// <summary>Creates a shipping profile. Requires the <c>shops_w</c> OAuth scope.</summary>
    Task<ApiResponse<ShopShippingProfile>> CreateShopShippingProfileAsync(string accessToken, long shopId, CreateShopShippingProfileRequest profile, CancellationToken cancellationToken = default);

    /// <summary>Gets a shop's shipping profiles. Requires the <c>shops_r</c> OAuth scope.</summary>
    Task<ApiResponse<EtsyListResponse<ShopShippingProfile>>> GetShopShippingProfilesAsync(string accessToken, long shopId, CancellationToken cancellationToken = default);

    /// <summary>Deletes a shipping profile. Requires the <c>shops_w</c> OAuth scope.</summary>
    Task<ApiResponse<object>> DeleteShopShippingProfileAsync(string accessToken, long shopId, long shippingProfileId, CancellationToken cancellationToken = default);

    /// <summary>Gets a shipping profile. Requires the <c>shops_r</c> OAuth scope.</summary>
    Task<ApiResponse<ShopShippingProfile>> GetShopShippingProfileAsync(string accessToken, long shopId, long shippingProfileId, CancellationToken cancellationToken = default);

    /// <summary>Updates a shipping profile. Requires the <c>shops_w</c> OAuth scope.</summary>
    Task<ApiResponse<ShopShippingProfile>> UpdateShopShippingProfileAsync(string accessToken, long shopId, long shippingProfileId, UpdateShopShippingProfileRequest profile, CancellationToken cancellationToken = default);

    /// <summary>Creates a shipping destination. Requires the <c>shops_w</c> OAuth scope.</summary>
    Task<ApiResponse<ShopShippingProfileDestination>> CreateShopShippingProfileDestinationAsync(string accessToken, long shopId, long shippingProfileId, CreateShopShippingProfileDestinationRequest destination, CancellationToken cancellationToken = default);

    /// <summary>Gets a shipping profile's destinations. Requires the <c>shops_r</c> OAuth scope.</summary>
    Task<ApiResponse<EtsyListResponse<ShopShippingProfileDestination>>> GetShopShippingProfileDestinationsAsync(string accessToken, long shopId, long shippingProfileId, GetShopShippingProfileDestinationsFilter? filter = null, CancellationToken cancellationToken = default);

    /// <summary>Deletes a shipping destination. Requires the <c>shops_w</c> OAuth scope.</summary>
    Task<ApiResponse<object>> DeleteShopShippingProfileDestinationAsync(string accessToken, long shopId, long shippingProfileId, long shippingProfileDestinationId, CancellationToken cancellationToken = default);

    /// <summary>Updates a shipping destination. Requires the <c>shops_w</c> OAuth scope.</summary>
    Task<ApiResponse<ShopShippingProfileDestination>> UpdateShopShippingProfileDestinationAsync(string accessToken, long shopId, long shippingProfileId, long shippingProfileDestinationId, UpdateShopShippingProfileDestinationRequest destination, CancellationToken cancellationToken = default);

    /// <summary>Creates a shipping profile upgrade. Requires the <c>shops_w</c> OAuth scope.</summary>
    Task<ApiResponse<ShopShippingProfileUpgrade>> CreateShopShippingProfileUpgradeAsync(string accessToken, long shopId, long shippingProfileId, CreateShopShippingProfileUpgradeRequest upgrade, CancellationToken cancellationToken = default);

    /// <summary>Gets a shipping profile's upgrades. Requires the <c>shops_r</c> OAuth scope.</summary>
    Task<ApiResponse<EtsyListResponse<ShopShippingProfileUpgrade>>> GetShopShippingProfileUpgradesAsync(string accessToken, long shopId, long shippingProfileId, CancellationToken cancellationToken = default);

    /// <summary>Deletes a shipping profile upgrade. Requires the <c>shops_w</c> OAuth scope.</summary>
    Task<ApiResponse<object>> DeleteShopShippingProfileUpgradeAsync(string accessToken, long shopId, long shippingProfileId, long upgradeId, CancellationToken cancellationToken = default);

    /// <summary>Updates a shipping profile upgrade. Requires the <c>shops_w</c> OAuth scope.</summary>
    Task<ApiResponse<ShopShippingProfileUpgrade>> UpdateShopShippingProfileUpgradeAsync(string accessToken, long shopId, long shippingProfileId, long upgradeId, UpdateShopShippingProfileUpgradeRequest upgrade, CancellationToken cancellationToken = default);
}
