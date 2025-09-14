using Newtonsoft.Json;

namespace Janice.Core.Models
{
    /// <summary>
    /// Represents a request to pull a model using the /api/pull endpoint.
    /// </summary>
    public class OllamaPullRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("stream")]
        public bool Stream { get; set; } = false; // Streaming responses for pulls can be complex, defaulting to false.
    }
}
