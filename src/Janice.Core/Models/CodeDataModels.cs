using Janet.Core.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Janet.Core.Models
{
    /// <summary>
    /// Defines the role of the entity responsible for a given message or request.
    /// </summary>


    /// <summary>
    /// Specifies the nature of the content within a ChatResponse payload.
    /// </summary>


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

    /// <summary>
    /// Represents the system's response to a ChatRequest.
    /// </summary>
    public class ChatResponse
    {
        [JsonProperty("responseType")]
        public ResponseType ResponseType { get; set; }

        [JsonProperty("payload")]
        public object Payload { get; set; } = new object();
    }

    /// <summary>
    /// Represents the parsed intent and entities from a user's request.
    /// </summary>
    public class Intention
    {
        [JsonProperty("intent")]
        public string Intent { get; set; } = string.Empty;

        [JsonProperty("entities")]
        public Dictionary<string, string> Entities { get; set; } = new Dictionary<string, string>();
    }

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
