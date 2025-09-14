using Newtonsoft.Json;
using System.Collections.Generic;

namespace Janice.Core.Models
{
    /// <summary>
    /// Represents the response from the Ollama /api/tags endpoint, containing a list of local models.
    /// </summary>
    public class OllamaTagsResponse
    {
        [JsonProperty("models")]
        public List<OllamaModel> Models { get; set; } = new List<OllamaModel>();
    }
}
