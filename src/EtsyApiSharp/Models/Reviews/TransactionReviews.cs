using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //A set of transaction review records left by Users.
    public class TransactionReviews
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<TransactionReview> Results { get; set; }


    }
}
