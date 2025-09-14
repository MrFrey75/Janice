using Newtonsoft.Json;
using System;

namespace Janice.Core.Models
{
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
}
