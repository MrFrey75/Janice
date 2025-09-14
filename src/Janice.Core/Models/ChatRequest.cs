using Janice.Core.Enums;
using Newtonsoft.Json;
using System;

namespace Janice.Core.Models
{
    /// <summary>
    /// Represents a single request from the user to the system.
    /// </summary>
    public class ChatRequest
    {
        [JsonProperty("role")]
        public Role Role { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; } = string.Empty;

        [JsonProperty("payload")]
        public object? Payload { get; set; }
    }
}
