using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models;

/// <summary>
/// A shop created by an Etsy user.
/// </summary>
public class Shop
{
    /// <summary>
    /// The unique positive non-zero numeric ID for an Etsy Shop.
    /// </summary>
    [JsonPropertyName("shop_id")]
    public long ShopId { get; set; }

    /// <summary>
    /// The numeric user ID of the [user](/documentation/reference#tag/User) who owns this shop.
    /// </summary>
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// The shop's name string.
    /// </summary>
    [JsonPropertyName("shop_name")]
    public string ShopName { get; set; } = string.Empty;

    /// <summary>
    /// The date and time this shop was created, in epoch seconds.
    /// </summary>
    [JsonPropertyName("create_date")]
    public long CreateDate { get; set; }

    /// <summary>
    /// The date and time this shop was created, in epoch seconds.
    /// </summary>
    [JsonPropertyName("created_timestamp")]
    public long CreatedTimestamp { get; set; }

    /// <summary>
    /// A brief heading string for the shop\'s main page.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// An announcement string to buyers that displays on the shop's homepage.
    /// </summary>
    [JsonPropertyName("announcement")]
    public string? Announcement { get; set; }

    /// <summary>
    /// The ISO (alphabetic) code for the shop's currency. The shop displays all prices in this currency by default.
    /// </summary>
    [JsonPropertyName("currency_code")]
    public string CurrencyCode { get; set; } = string.Empty;

    /// <summary>
    /// When true, this shop is not accepting purchases.
    /// </summary>
    [JsonPropertyName("is_vacation")]
    public bool IsVacation { get; set; }

    /// <summary>
    /// The shop's message string displayed when is_vacation is true.
    /// </summary>
    [JsonPropertyName("vacation_message")]
    public string? VacationMessage { get; set; }

    /// <summary>
    /// A message string sent to users who complete a purchase from this shop.
    /// </summary>
    [JsonPropertyName("sale_message")]
    public string? SaleMessage { get; set; }

    /// <summary>
    /// A message string sent to users who purchase a digital item from this shop.
    /// </summary>
    [JsonPropertyName("digital_sale_message")]
    public string? DigitalSaleMessage { get; set; }

    /// <summary>
    /// The date and time of the last update to the shop, in epoch seconds.
    /// </summary>
    [JsonPropertyName("update_date")]
    public long UpdateDate { get; set; }

    /// <summary>
    /// The date and time this shop was last updated, in epoch seconds.
    /// </summary>
    [JsonPropertyName("updated_timestamp")]
    public long UpdatedTimestamp { get; set; }

    /// <summary>
    /// The number of active listings in the shop.
    /// </summary>
    [JsonPropertyName("listing_active_count")]
    public long ListingActiveCount { get; set; }

    /// <summary>
    /// The number of digital listings in the shop.
    /// </summary>
    [JsonPropertyName("digital_listing_count")]
    public long DigitalListingCount { get; set; }

    /// <summary>
    /// The shop owner\'s login name string.
    /// </summary>
    [JsonPropertyName("login_name")]
    public string LoginName { get; set; } = string.Empty;

    /// <summary>
    /// When true, the shop accepts customization requests.
    /// </summary>
    [JsonPropertyName("accepts_custom_requests")]
    public bool AcceptsCustomRequests { get; set; }

    /// <summary>
    /// The shop's policy welcome string (may be blank).
    /// </summary>
    [JsonPropertyName("policy_welcome")]
    public string? PolicyWelcome { get; set; }

    /// <summary>
    /// The shop's payment policy string (may be blank).
    /// </summary>
    [JsonPropertyName("policy_payment")]
    public string? PolicyPayment { get; set; }

    /// <summary>
    /// The shop's shipping policy string (may be blank).
    /// </summary>
    [JsonPropertyName("policy_shipping")]
    public string? PolicyShipping { get; set; }

    /// <summary>
    /// The shop's refund policy string (may be blank).
    /// </summary>
    [JsonPropertyName("policy_refunds")]
    public string? PolicyRefunds { get; set; }

    /// <summary>
    /// The shop's additional policies string (may be blank).
    /// </summary>
    [JsonPropertyName("policy_additional")]
    public string? PolicyAdditional { get; set; }

    /// <summary>
    /// The shop's seller infomation string (may be blank).
    /// </summary>
    [JsonPropertyName("policy_seller_info")]
    public string? PolicySellerInfo { get; set; }

    /// <summary>
    /// The date and time of the last update to the shop's policies, in epoch seconds.
    /// </summary>
    [JsonPropertyName("policy_update_date")]
    public long PolicyUpdateDate { get; set; }

    /// <summary>
    /// When true, EU receipts display private info.
    /// </summary>
    [JsonPropertyName("policy_has_private_receipt_info")]
    public bool PolicyHasPrivateReceiptInfo { get; set; }

    /// <summary>
    /// When true, the shop displays additional unstructured policy fields.
    /// </summary>
    [JsonPropertyName("has_unstructured_policies")]
    public bool HasUnstructuredPolicies { get; set; }

    /// <summary>
    /// The shop's privacy policy string (may be blank).
    /// </summary>
    [JsonPropertyName("policy_privacy")]
    public string? PolicyPrivacy { get; set; }

    /// <summary>
    /// The shop's automatic reply string displayed in new conversations when is_vacation is true.
    /// </summary>
    [JsonPropertyName("vacation_autoreply")]
    public string? VacationAutoreply { get; set; }

    /// <summary>
    /// The URL string for this shop.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// The URL string for this shop's banner image.
    /// </summary>
    [JsonPropertyName("image_url_760x100")]
    public string? ImageUrl760X100 { get; set; }

    /// <summary>
    /// The number of users who marked this shop a favorite.
    /// </summary>
    [JsonPropertyName("num_favorers")]
    public long NumFavorers { get; set; }

    /// <summary>
    /// A list of language strings for the shop's enrolled languages.
    /// </summary>
    [JsonPropertyName("languages")]
    public List<string> Languages { get; set; } = new();

    /// <summary>
    /// The URL string for this shop's icon image.
    /// </summary>
    [JsonPropertyName("icon_url_fullxfull")]
    public string? IconUrlFullxfull { get; set; }

    /// <summary>
    /// When true, the shop accepted using structured policies.
    /// </summary>
    [JsonPropertyName("is_using_structured_policies")]
    public bool IsUsingStructuredPolicies { get; set; }

    /// <summary>
    /// When true, the shop accepted OR declined after viewing structured policies onboarding.
    /// </summary>
    [JsonPropertyName("has_onboarded_structured_policies")]
    public bool HasOnboardedStructuredPolicies { get; set; }

    /// <summary>
    /// When true, this shop\'s policies include a link to an EU online dispute form.
    /// </summary>
    [JsonPropertyName("include_dispute_form_link")]
    public bool IncludeDisputeFormLink { get; set; }

    /// <summary>
    /// (**DEPRECATED: Replaced by _is_etsy_payments_onboarded._) When true, the shop has onboarded onto Etsy Payments.
    /// </summary>
    [JsonPropertyName("is_direct_checkout_onboarded")]
    public bool IsDirectCheckoutOnboarded { get; set; }

    /// <summary>
    /// When true, the shop has onboarded onto Etsy Payments.
    /// </summary>
    [JsonPropertyName("is_etsy_payments_onboarded")]
    public bool IsEtsyPaymentsOnboarded { get; set; }

    /// <summary>
    /// When true, the shop is elegible for calculated shipping profiles. (Only available in the US and Canada)
    /// </summary>
    [JsonPropertyName("is_calculated_eligible")]
    public bool IsCalculatedEligible { get; set; }

    /// <summary>
    /// When true, the shop opted in to buyer promise.
    /// </summary>
    [JsonPropertyName("is_opted_in_to_buyer_promise")]
    public bool IsOptedInToBuyerPromise { get; set; }

    /// <summary>
    /// When true, the shop is based in the US.
    /// </summary>
    [JsonPropertyName("is_shop_us_based")]
    public bool IsShopUsBased { get; set; }

    /// <summary>
    /// The total number of sales ([transactions](/documentation/reference#tag/Shop-Receipt-Transactions)) for this shop
    /// </summary>
    [JsonPropertyName("transaction_sold_count")]
    public long TransactionSoldCount { get; set; }

    /// <summary>
    /// The country iso the shop is shipping from.
    /// </summary>
    [JsonPropertyName("shipping_from_country_iso")]
    public string? ShippingFromCountryIso { get; set; }

    /// <summary>
    /// The country iso where the shop is located.
    /// </summary>
    [JsonPropertyName("shop_location_country_iso")]
    public string? ShopLocationCountryIso { get; set; }

    /// <summary>
    /// Number of reviews of shop listings in the past year.
    /// </summary>
    [JsonPropertyName("review_count")]
    public long? ReviewCount { get; set; }

    /// <summary>
    /// Average rating based on reviews of shop listings in the past year.
    /// </summary>
    [JsonPropertyName("review_average")]
    public float? ReviewAverage { get; set; }

}
