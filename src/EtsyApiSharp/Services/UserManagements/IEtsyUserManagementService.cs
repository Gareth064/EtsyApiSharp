using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;

namespace EtsyApiSharp.Services.UserManagements;
/// <summary>
/// Represents I Etsy User Management Service.
/// </summary>

public interface IEtsyUserManagementService
{
    /// <summary>
    /// Retrieves an accessible Etsy user profile. Requires the <c>email_r</c> OAuth scope and is
    /// limited to the authenticated user or a linked buyer.
    /// </summary>
    Task<ApiResponse<User>> GetUserAsync(
        string accessToken,
        long userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the user and shop IDs associated with the access token. Requires the <c>shops_r</c> OAuth scope.
    /// </summary>
    Task<ApiResponse<Self>> GetMeAsync(
        string accessToken,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the authenticated user's shipping addresses. Requires the <c>address_r</c> OAuth scope.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<UserAddress>>> GetUserAddressesAsync(
        string accessToken,
        GetUserAddressesFilter? filter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves one of the authenticated user's shipping addresses. Requires the <c>address_r</c> OAuth scope.
    /// </summary>
    Task<ApiResponse<UserAddress>> GetUserAddressAsync(
        string accessToken,
        long userAddressId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes one of the authenticated user's shipping addresses. Etsy currently declares the
    /// <c>address_r</c> OAuth scope for this operation.
    /// </summary>
    Task<ApiResponse<object>> DeleteUserAddressAsync(
        string accessToken,
        long userAddressId,
        CancellationToken cancellationToken = default);
}
