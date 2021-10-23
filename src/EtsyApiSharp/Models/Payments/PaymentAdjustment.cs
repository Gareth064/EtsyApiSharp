using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //Represents a refund, which applies to a prior Etsy payment. All monetary amounts are in USD pennies unless otherwise specified.
    public class PaymentAdjustment
    {
        [JsonPropertyName("payment_adjustment_id")]
        public int PaymentAdjustmentId { get; set; }


        [JsonPropertyName("payment_id")]
        public int PaymentId { get; set; }


        [JsonPropertyName("status")]
        public string Status { get; set; }


        [JsonPropertyName("is_success")]
        public bool IsSuccess { get; set; }


        [JsonPropertyName("user_id")]
        public int UserId { get; set; }


        [JsonPropertyName("reason_code")]
        public string ReasonCode { get; set; }


        [JsonPropertyName("total_adjustment_amount")]
        public int TotalAdjustmentAmount { get; set; }


        [JsonPropertyName("shop_total_adjustment_amount")]
        public int ShopTotalAdjustmentAmount { get; set; }


        [JsonPropertyName("buyer_total_adjustment_amount")]
        public int BuyerTotalAdjustmentAmount { get; set; }


        [JsonPropertyName("total_fee_adjustment_amount")]
        public int TotalFeeAdjustmentAmount { get; set; }


        [JsonPropertyName("create_timestamp")]
        public int CreateTimestamp { get; set; }


        [JsonPropertyName("update_timestamp")]
        public int UpdateTimestamp { get; set; }


    }
}
