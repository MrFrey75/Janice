using Newtonsoft.Json;
using System;

namespace Janice.Core.Models
{
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
}
