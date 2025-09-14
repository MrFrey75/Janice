public class JanetConfig
{
    public string OllamaApiUrl { get; set; } = "http://localhost:11434";
    public string DefaultModel { get; set; } = "phi3:latest";
    public int MaxTokens { get; set; } = 1024;
    public double Temperature { get; set; } = 0.7;
    public double TopP { get; set; } = 0.9;
    public int N { get; set; } = 1;
    public bool Stream { get; set; } = false;
    public int TimeoutSeconds { get; set; } = 60;
}