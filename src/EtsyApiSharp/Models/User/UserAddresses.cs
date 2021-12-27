using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //Represents several UserAddress records.
    public class UserAddresses
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<UserAddress> Results { get; set; }


    }
}
