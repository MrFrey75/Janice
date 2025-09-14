using Newtonsoft.Json;
using System;

namespace Janice.Core.Models
{
    /// <summary>
    /// Represents a single turn in a conversation, capturing both the request and the response.
    /// </summary>
    public class ChatInteraction
    {
        [JsonProperty("uid")]
        public Guid UID { get; set; } = Guid.NewGuid();

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonProperty("request")]
        public ChatRequest Request { get; set; } = new ChatRequest();

        [JsonProperty("response")]
        public ChatResponse Response { get; set; } = new ChatResponse();

        [JsonProperty("intention")]
        public Intention Intention { get; set; } = new Intention();
    }
}
