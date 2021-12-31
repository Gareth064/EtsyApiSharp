using System.Text.Json.Serialization;

namespace EtsyApiSharp.Models
{
    /// <summary>
    /// A confirmation that the current application has access to the Open API
    /// </summary>
    public class Pong
    {
        /// <summary>
        /// The authenticated application's ID
        /// </summary>
        [JsonPropertyName("application_id")]
        public long ApplicationId { get; set; }

    }
}
