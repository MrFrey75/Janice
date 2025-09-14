using Janice.Core.Models;
using Janice.Core.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Janice.Core.Services
{
    /// <summary>
    /// A service for interacting with the Ollama API.
    /// </summary>
    public class OllamaApiService : IOllamaApiService
    {
        private readonly HttpClient _httpClient;
        private const string DefaultModel = "phi3";

        /// <inheritdoc/>
        public List<OllamaModel> AvailableModels { get; private set; } = new List<OllamaModel>();

        /// <inheritdoc/>
        public OllamaModel? CurrentModel { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OllamaApiService"/> class.
        /// </summary>
        /// <param name="httpClient">The HttpClient instance to use for making API calls.</param>
        public OllamaApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc/>
        public async Task InitAsync(CancellationToken cancellationToken = default)
        {
            AvailableModels = await GetOllamaModelsAsync(cancellationToken);
            var defaultModel = AvailableModels.FirstOrDefault(m => m.Name.Contains(DefaultModel));

            if (defaultModel != null)
            {
                SetOllamaModel(defaultModel);
            }
            else if (AvailableModels.Any())
            {
                SetOllamaModel(AvailableModels.First());
            }
            else
            {
                CurrentModel = null;
            }
        }

        /// <inheritdoc/>
        public async Task<List<OllamaModel>> GetOllamaModelsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var httpResponse = await _httpClient.GetAsync("/api/tags", cancellationToken);
                httpResponse.EnsureSuccessStatusCode();

                var jsonResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
                var tagsResponse = JsonConvert.DeserializeObject<OllamaTagsResponse>(jsonResponse);

                return tagsResponse?.Models ?? new List<OllamaModel>();
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to get models from Ollama API. Please ensure Ollama is running.", ex);
            }
        }

        /// <inheritdoc/>
        public void SetOllamaModel(OllamaModel model)
        {
            CurrentModel = model ?? throw new ArgumentNullException(nameof(model));
        }

        /// <inheritdoc/>
        public async Task PullModelAsync(string modelName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(modelName))
            {
                throw new ArgumentException("Model name cannot be empty.", nameof(modelName));
            }
            
            try
            {
                var pullRequest = new OllamaPullRequest { Name = modelName };
                var jsonRequest = JsonConvert.SerializeObject(pullRequest);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var httpResponse = await _httpClient.PostAsync("/api/pull", content, cancellationToken);
                httpResponse.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException($"Failed to pull model '{modelName}'. Please ensure Ollama is running and the model name is correct.", ex);
            }
        }

        /// <inheritdoc/>
        public async Task<OllamaChatResponse> SendChatAsync(OllamaChatRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.Model))
            {
                if (CurrentModel == null)
                {
                    throw new InvalidOperationException("No model is selected for the chat. Please initialize the service and set a model first.");
                }
                request.Model = CurrentModel.Name;
            }

            try
            {
                var jsonRequest = JsonConvert.SerializeObject(request,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var httpResponse = await _httpClient.PostAsync("/api/chat", content, cancellationToken);

                httpResponse.EnsureSuccessStatusCode();

                var jsonResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
                
                var ollamaResponse = JsonConvert.DeserializeObject<OllamaChatResponse>(jsonResponse);

                if (ollamaResponse == null)
                {
                    throw new InvalidOperationException("Failed to deserialize the response from the Ollama API.");
                }

                return ollamaResponse;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("An error occurred while communicating with the Ollama API. Please ensure Ollama is running.", ex);
            }
            catch (JsonException ex)
            {
                throw new ApplicationException("An error occurred while processing the response from the Ollama API.", ex);
            }
        }
    }
}

