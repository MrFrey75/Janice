using Janice.Core.Models;
namespace Janice.Core.Services.Interfaces
{
    public interface IConfigService
    {
        JanetConfig Config { get; }
        string OllamaApiUrl { get; }
        string DefaultModel { get; }
        int MaxTokens { get; }
        double Temperature { get; }
        double TopP { get; }
        int N { get; }
        bool Stream { get; }
        int TimeoutSeconds { get; }

        event EventHandler? ConfigReloaded;

        Task ReloadAsync();
        Task UpdateConfigAsync(Action<JanetConfig> updateAction);
    }
}
