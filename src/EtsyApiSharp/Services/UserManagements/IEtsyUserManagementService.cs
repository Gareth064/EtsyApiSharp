using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;

namespace EtsyApiSharp.Services.UserManagements;

public interface IEtsyUserManagementService
{
    /// <summary>
    /// Retrieves an accessible Etsy user profile. Requires the <c>email_r</c> OAuth scope.
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
}
