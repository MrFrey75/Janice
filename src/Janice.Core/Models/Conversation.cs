using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Janice.Core.Models
{
    /// <summary>
    /// Represents a complete conversation thread, containing multiple interactions.
    /// </summary>
    public class Conversation
    {
        [JsonProperty("uid")]
        public Guid UID { get; set; } = Guid.NewGuid();

        [JsonProperty("title")]
        public string Title { get; set; } = "New Conversation";

        [JsonProperty("summary")]
        public string Summary { get; set; } = string.Empty;

        [JsonProperty("user")]
        public Guid User { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonProperty("modifiedAt")]
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

        [JsonProperty("interactions")]
        public ICollection<ChatInteraction> Interactions { get; set; } = new List<ChatInteraction>();
    }
}
