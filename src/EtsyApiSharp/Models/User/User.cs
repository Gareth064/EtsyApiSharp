using System.Text.Json.Serialization;


namespace EtsyApiSharp.Models
{
    //Represents a single user of the site
    public class User
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }


        [JsonPropertyName("login_name")]
        public string LoginName { get; set; }


        [JsonPropertyName("primary_email")]
        public string PrimaryEmail { get; set; }


        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }


        [JsonPropertyName("last_name")]
        public string LastName { get; set; }


        [JsonPropertyName("create_timestamp")]
        public int CreateTimestamp { get; set; }


        [JsonPropertyName("referred_by_user_id")]
        public int ReferredByUserId { get; set; }


        [JsonPropertyName("use_new_inventory_endpoints")]
        public bool UseNewInventoryEndpoints { get; set; }


        [JsonPropertyName("is_seller")]
        public bool IsSeller { get; set; }


        [JsonPropertyName("bio")]
        public string Bio { get; set; }


        [JsonPropertyName("gender")]
        public string Gender { get; set; }


        [JsonPropertyName("birth_month")]
        public string BirthMonth { get; set; }


        [JsonPropertyName("birth_day")]
        public string BirthDay { get; set; }


        [JsonPropertyName("transaction_buy_count")]
        public int TransactionBuyCount { get; set; }


        [JsonPropertyName("transaction_sold_count")]
        public int TransactionSoldCount { get; set; }


    }
}
