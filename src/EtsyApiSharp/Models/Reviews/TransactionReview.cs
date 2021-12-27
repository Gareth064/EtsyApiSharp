using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //A transaction review record left by a User.
    public class TransactionReview
    {
        [JsonPropertyName("shop_id")]
        public int ShopId { get; set; }


        [JsonPropertyName("listing_id")]
        public int ListingId { get; set; }


        [JsonPropertyName("transaction_id")]
        public int TransactionId { get; set; }


        [JsonPropertyName("buyer_user_id")]
        public int BuyerUserId { get; set; }


        [JsonPropertyName("rating")]
        public int Rating { get; set; }


        [JsonPropertyName("review")]
        public string Review { get; set; }


        [JsonPropertyName("language")]
        public string Language { get; set; }


        [JsonPropertyName("image_url_fullxfull")]
        public string ImageUrlFullxfull { get; set; }


        [JsonPropertyName("create_timestamp")]
        public int CreateTimestamp { get; set; }


        [JsonPropertyName("update_timestamp")]
        public int UpdateTimestamp { get; set; }


    }
}
