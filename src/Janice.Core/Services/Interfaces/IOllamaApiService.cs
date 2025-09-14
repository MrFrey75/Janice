using Janet.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Janet.Core.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for a service that communicates with the Ollama API.
    /// </summary>
    public interface IOllamaApiService
    {
        /// <summary>
        /// Gets the list of models available in the Ollama instance after initialization.
        /// </summary>
        List<OllamaModel> AvailableModels { get; }
        
        /// <summary>
        /// Gets the model currently selected for general chat operations.
        /// </summary>
        OllamaModel? CurrentModel { get; }

        /// <summary>
        /// Initializes the service by fetching available models and setting a default.
        /// </summary>
        Task InitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the list of all models currently installed in the Ollama instance.
        /// </summary>
        /// <returns>A list of <see cref="OllamaModel"/>.</returns>
        Task<List<OllamaModel>> GetOllamaModelsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets the specified model as the current model for subsequent chat requests.
        /// </summary>
        /// <param name="model">The model to set as current.</param>
        void SetOllamaModel(OllamaModel model);

        /// <summary>
        /// Pulls a model from the Ollama library.
        /// </summary>
        /// <param name="modelName">The name of the model to pull (e.g., "phi3:latest").</param>
        /// <param name="cancellationToken"></param>
        Task PullModelAsync(string modelName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a chat request to the Ollama API and retrieves the response.
        /// </summary>
        /// <param name="request">The chat request object.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>An <see cref="OllamaChatResponse"/> containing the model's reply.</returns>
        Task<OllamaChatResponse> SendChatAsync(OllamaChatRequest request, CancellationToken cancellationToken = default);
    }
}

