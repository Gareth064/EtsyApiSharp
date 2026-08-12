using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;

namespace EtsyApiSharp.Services.ReviewManagements;

/// <summary>
/// Provides access to Etsy Review Management resources.
/// </summary>
public interface IEtsyReviewManagementService
{
    /// <summary>
    /// Retrieves public reviews for a listing. This operation requires an Etsy API key but no OAuth scope.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ListingReview>>> GetReviewsByListingAsync(
        long listingId,
        GetReviewsFilter? filter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves public reviews for a shop. This operation requires an Etsy API key but no OAuth scope.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<TransactionReview>>> GetReviewsByShopAsync(
        long shopId,
        GetReviewsFilter? filter = null,
        CancellationToken cancellationToken = default);
}
