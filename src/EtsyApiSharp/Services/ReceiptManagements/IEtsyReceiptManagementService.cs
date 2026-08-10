using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;

namespace EtsyApiSharp.Services.ReceiptManagements;

public interface IEtsyReceiptManagementService
{
    /// <summary>
    /// Retrieves a receipt from an Etsy shop. Requires the <c>transactions_r</c> OAuth scope.
    /// </summary>
    Task<ApiResponse<ShopReceipt>> GetShopReceiptAsync(
        string accessToken,
        long shopId,
        long receiptId,
        bool? legacy = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves receipts from an Etsy shop. Requires the <c>transactions_r</c> OAuth scope.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ShopReceipt>>> GetShopReceiptsAsync(
        string accessToken,
        long shopId,
        GetShopReceiptsFilter? filter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the paid or shipped state of an Etsy receipt. Requires the <c>transactions_w</c> OAuth scope.
    /// </summary>
    Task<ApiResponse<ShopReceipt>> UpdateShopReceiptAsync(
        string accessToken,
        long shopId,
        long receiptId,
        UpdateShopReceiptRequest update,
        bool? legacy = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates shipment tracking information for an Etsy receipt. Requires the <c>transactions_w</c> OAuth scope.
    /// </summary>
    Task<ApiResponse<ShopReceipt>> CreateReceiptShipmentAsync(
        string accessToken,
        long shopId,
        long receiptId,
        CreateReceiptShipmentRequest shipment,
        bool? legacy = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a receipt transaction by ID. Requires the <c>transactions_r</c> OAuth scope.
    /// </summary>
    Task<ApiResponse<ShopReceiptTransaction>> GetShopReceiptTransactionAsync(
        string accessToken,
        long shopId,
        long transactionId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves receipt transactions associated with a listing. Requires the <c>transactions_r</c> OAuth scope.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ShopReceiptTransaction>>> GetShopReceiptTransactionsByListingAsync(
        string accessToken,
        long shopId,
        long listingId,
        GetShopReceiptTransactionsByListingFilter? filter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves transactions associated with a receipt. Requires the <c>transactions_r</c> OAuth scope.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ShopReceiptTransaction>>> GetShopReceiptTransactionsByReceiptAsync(
        string accessToken,
        long shopId,
        long receiptId,
        bool? legacy = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves transactions associated with an Etsy shop. Requires the <c>transactions_r</c> OAuth scope.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<ShopReceiptTransaction>>> GetShopReceiptTransactionsByShopAsync(
        string accessToken,
        long shopId,
        GetShopReceiptTransactionsByShopFilter? filter = null,
        CancellationToken cancellationToken = default);
}
