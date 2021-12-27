using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models.Common
{
    public class EtsyListResponse<T>
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("results")]
        public List<T> Results { get; set; }
    }
}
