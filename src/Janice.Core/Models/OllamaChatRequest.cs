using Newtonsoft.Json;
using System.Collections.Generic;

namespace Janice.Core.Models
{
    /// <summary>
    /// Represents a request to the Ollama /api/chat endpoint.
    /// </summary>
    public class OllamaChatRequest
    {
        [JsonProperty("model")]
        public string Model { get; set; } = string.Empty;

        [JsonProperty("messages")]
        public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

        [JsonProperty("stream")]
        public bool Stream { get; set; } = false;

        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object>? Options { get; set; }
    }
}
