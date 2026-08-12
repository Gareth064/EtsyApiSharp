using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;

namespace EtsyApiSharp.Services.PaymentManagements;

/// <summary>
/// Provides access to Etsy Payment Management resources.
/// </summary>
public interface IEtsyPaymentManagementService
{
    /// <summary>
    /// Retrieves one payment-account ledger entry. Requires the <c>transactions_r</c> OAuth scope.
    /// </summary>
    Task<ApiResponse<PaymentAccountLedgerEntry>> GetShopPaymentAccountLedgerEntryAsync(
        string accessToken,
        long shopId,
        long ledgerEntryId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves payment-account ledger entries in a required creation-time range. Requires the <c>transactions_r</c> OAuth scope.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<PaymentAccountLedgerEntry>>> GetShopPaymentAccountLedgerEntriesAsync(
        string accessToken,
        long shopId,
        GetShopPaymentAccountLedgerEntriesFilter filter,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves payments associated with payment-account ledger entries. Requires the <c>transactions_r</c> OAuth scope.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<Payment>>> GetPaymentAccountLedgerEntryPaymentsAsync(
        string accessToken,
        long shopId,
        IReadOnlyCollection<long> ledgerEntryIds,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the payment for a receipt. Requires the <c>transactions_r</c> OAuth scope.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<Payment>>> GetShopPaymentByReceiptIdAsync(
        string accessToken,
        long shopId,
        long receiptId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves payments from a shop by their IDs. Requires the <c>transactions_r</c> OAuth scope.
    /// </summary>
    Task<ApiResponse<EtsyListResponse<Payment>>> GetPaymentsAsync(
        string accessToken,
        long shopId,
        IReadOnlyCollection<long> paymentIds,
        CancellationToken cancellationToken = default);
}
