using Newtonsoft.Json;
using System.Collections.Generic;

namespace Janice.Core.Models
{
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
}
