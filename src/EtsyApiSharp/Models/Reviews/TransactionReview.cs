using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    /// <summary>
    /// A transaction review record left by a User.
    /// </summary>
    public class TransactionReview
    {
        /// <summary>
        /// The shop's numeric ID.
        /// </summary>
        [JsonPropertyName("shop_id")]
        public long ShopId { get; set; }

        /// <summary>
        /// The ID of the ShopListing that the TransactionReview belongs to.
        /// </summary>
        [JsonPropertyName("listing_id")]
        public long ListingId { get; set; }

        /// <summary>
        /// The ID of the ShopReceipt Transaction that the TransactionReview belongs to.
        /// </summary>
        [JsonPropertyName("transaction_id")]
        public long TransactionId { get; set; }

        /// <summary>
        /// The numeric ID of the user who was the buyer in this transaction. Note: This field may be absent, depending on the buyer's privacy settings.
        /// </summary>
        [JsonPropertyName("buyer_user_id")]
        public long? BuyerUserId { get; set; }

        /// <summary>
        /// Rating value on scale from 1 to 5
        /// </summary>
        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        /// <summary>
        /// A message left by the author, explaining the feedback.
        /// </summary>
        [JsonPropertyName("review")]
        public string? Review { get; set; }

        /// <summary>
        /// The language of the TransactionReview
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; }

        /// <summary>
        /// The url to a photo provided with the feedback, dimensions fullxfull. Note: This field may be absent, depending on the buyer's privacy settings.
        /// </summary>
        [JsonPropertyName("image_url_fullxfull")]
        public string ImageUrlFullxfull { get; set; }

        /// <summary>
        /// The date and time the TransactionReview was created in epoch seconds.
        /// </summary>
        [JsonPropertyName("create_timestamp")]
        public int CreateTimestamp { get; set; }

        /// <summary>
        /// The date and time the TransactionReview was updated in epoch seconds.
        /// </summary>
        [JsonPropertyName("update_timestamp")]
        public int UpdateTimestamp { get; set; }

    }
}
