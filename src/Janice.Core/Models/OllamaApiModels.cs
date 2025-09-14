using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Janet.Core.Models
{
    /// <summary>
    /// Represents a request to the Ollama /api/chat endpoint.
    /// </summary>
    public class OllamaChatRequest
    {
        /// <summary>
        /// Gets or sets the name of the model to use for the chat.
        /// </summary>
        [JsonProperty("model")]
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of messages forming the conversation history.
        /// </summary>
        [JsonProperty("messages")]
        public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

        /// <summary>
        /// Gets or sets a value indicating whether to stream the response. Defaults to false.
        /// </summary>
        [JsonProperty("stream")]
        public bool Stream { get; set; } = false;

        /// <summary>
        /// Gets or sets additional model parameters.
        /// </summary>
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object>? Options { get; set; }
    }

    /// <summary>
    /// Represents a response from the Ollama /api/chat endpoint.
    /// </summary>
    public class OllamaChatResponse
    {
        [JsonProperty("model")]
        public string Model { get; set; } = string.Empty;

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("message")]
        public ChatMessage Message { get; set; } = new ChatMessage();

        [JsonProperty("done")]
        public bool Done { get; set; }

        [JsonProperty("total_duration")]
        public long TotalDuration { get; set; }

        [JsonProperty("load_duration")]
        public long LoadDuration { get; set; }

        [JsonProperty("prompt_eval_count")]
        public int PromptEvalCount { get; set; }

        [JsonProperty("prompt_eval_duration")]
        public long PromptEvalDuration { get; set; }

        [JsonProperty("eval_count")]
        public int EvalCount { get; set; }

        [JsonProperty("eval_duration")]
        public long EvalDuration { get; set; }
    }
    
    /// <summary>
    /// Represents the response from the Ollama /api/tags endpoint, containing a list of local models.
    /// </summary>
    public class OllamaTagsResponse
    {
        [JsonProperty("models")]
        public List<OllamaModel> Models { get; set; } = new List<OllamaModel>();
    }

    /// <summary>
    /// Represents a single model available in the Ollama instance.
    /// </summary>
    public class OllamaModel
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("modified_at")]
        public DateTime ModifiedAt { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }
    }

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

