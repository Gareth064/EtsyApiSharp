using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    public class ErrorResponse
    {
        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}
