using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;

namespace EtsyApiSharp.Services.ShopManagements;

public interface IEtsyShopManagementService
{
    Task<ApiResponse<Shop>> GetShopByOwnerUserIdAsync(
        string apiToken,
        long userId);

    Task<ApiResponse<Shop>> GetShopAsync(
        string apiToken,
        long shopId);

    Task<ApiResponse<Shop>> UpdateShopAsync(
        string apiToken,
        long shopId,
        Shop shop);

    Task<ApiResponse<EtsyListResponse<Shop>>> FindShopsAsync(
        string shopName,
        FindShopsByNameFilter filter);
}
