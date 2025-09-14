using Janet.Core.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Janet.Core.Models
{
    /// <summary>
    /// Represents a single message in a chat conversation, compatible with the Ollama API.
    /// </summary>
    /// <remarks>
    /// This class is designed to be serialized into a JSON object that conforms to the message format expected by the Ollama /api/chat endpoint.
    /// A typical chat request would involve a list of these messages to provide conversation history.
    /// <example>
    /// A simple user message in JSON format:
    /// <code>
    /// {
    ///   "role": "user",
    ///   "content": "Hello, who are you?"
    /// }
    /// </code>
    /// </example>
    /// </remarks>
    public class ChatMessage
    {
        /// <summary>
        /// Gets or sets the role of the message author (e.g., "system", "user", or "assistant").
        /// </summary>
        /// <value>
        /// A string representing the role, typically "system", "user", or "assistant".
        /// </value>
        [JsonProperty("role")]
        public Role Role { get; set; } = Role.User;

        /// <summary>
        /// Gets or sets the text content of the message.
        /// </summary>
        /// <value>
        /// The string content of the message.
        /// </value>
        [JsonProperty("content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets an optional list of base64-encoded images for multimodal models.
        /// This property will not be serialized if it is null.
        /// </summary>
        /// <value>
        /// A list of strings, where each string is a base64-encoded image. Null if no images are included.
        /// </value>
        /// <remarks>
        /// This property is used for models that can process images along with text (multimodal).
        /// It will be omitted from the JSON output if the list is null.
        /// </remarks>
        [JsonProperty("images", NullValueHandling = NullValueHandling.Ignore)]
        public List<string>? Images { get; set; }
    }
}

