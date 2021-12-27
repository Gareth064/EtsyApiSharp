using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    //A confirmation that the current application has access to the Open API
    public class Pong
    {
        [JsonPropertyName("application_id")]
        public int ApplicationId { get; set; }


    }
}
