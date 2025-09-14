using Janice.Core.Enums;
using Newtonsoft.Json;
using System;

namespace Janice.Core.Models
{
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
}
