using YamlDotNet.Serialization;
namespace Janice.Core.Models
{
    public class JanetConfig
    {
        [YamlMember(Alias = "OllamaApiUrl")]
        public string OllamaApiUrl { get; set; } = "http://localhost:11434";

        [YamlMember(Alias = "DefaultModel")]
        public string DefaultModel { get; set; } = "phi3:latest";

        [YamlMember(Alias = "MaxTokens")]
        public int MaxTokens { get; set; } = 1024;

        [YamlMember(Alias = "Temperature")]
        public double Temperature { get; set; } = 0.7;

        [YamlMember(Alias = "TopP")]
        public double TopP { get; set; } = 0.9;

        [YamlMember(Alias = "N")]
        public int N { get; set; } = 1;

        [YamlMember(Alias = "Stream")]
        public bool Stream { get; set; } = false;

        [YamlMember(Alias = "TimeoutSeconds")]
        public int TimeoutSeconds { get; set; } = 60;
    }
}