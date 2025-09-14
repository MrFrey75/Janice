using Janice.Core.Enums;
using Janice.Core.Models;
using Janice.Core.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Janice.Core.Services;

/// <summary>
/// A service to determine user intent by leveraging an underlying language model.
/// </summary>
public class IntentService : IIntentService
{
    private readonly IOllamaApiService _ollamaApiService;
    private OllamaModel? _intentOllamaModel; 
    private const string IntentModelPreference = "phi3";

    private const string SystemPrompt = @"
You are an expert NLU (Natural Language Understanding) model. Your task is to identify the user's intent and extract relevant entities from their message.
You MUST respond with ONLY a JSON object in the following format:
{
  ""intent"": ""intent_name"",
  ""entities"": {
    ""entity_name_1"": ""entity_value_1"",
    ""entity_name_2"": ""entity_value_2""
  }
}

Example 1:
User: ""What's the weather like in New York?""
Your JSON response:
{
  ""intent"": ""get_weather"",
  ""entities"": {
    ""location"": ""New York""
  }
}

Example 2:
User: ""Remind me to buy milk at 5pm""
Your JSON response:
{
    ""intent"": ""set_reminder"",
    ""entities"": {
        ""task"": ""buy milk"",
        ""time"": ""5pm""
    }
}

If you cannot determine the intent, respond with:
{
  ""intent"": ""unknown"",
  ""entities"": {}
}

Do not add any explanations, markdown formatting, or introductory text. Only provide the raw JSON object.";

    /// <summary>
    /// Initializes a new instance of the <see cref="IntentService"/> class.
    /// </summary>
    /// <param name="ollamaApiService">The service for communicating with the Ollama API.</param>
    public IntentService(IOllamaApiService ollamaApiService)
    {
        _ollamaApiService = ollamaApiService;
    }

    /// <inheritdoc/>
    public async Task InitAsync(CancellationToken cancellationToken = default)
    {
        // The OllamaApiService should be initialized first by the application's startup logic.
        var models = _ollamaApiService.AvailableModels;
        if (!models.Any())
        {
            models = await _ollamaApiService.GetOllamaModelsAsync(cancellationToken);
        }

        _intentOllamaModel = models.FirstOrDefault(m => m.Name.Contains(IntentModelPreference)) 
                            ?? models.FirstOrDefault();
    }

    /// <inheritdoc/>
    public async Task<Intention> GetIntentionAsync(string userInput, CancellationToken cancellationToken = default)
    {
        if (_intentOllamaModel == null)
        {
            throw new InvalidOperationException("IntentService has not been initialized or no models are available.");
        }

        var messages = new List<ChatMessage>
        {
            new ChatMessage { Role = Role.System, Content = SystemPrompt },
            new ChatMessage { Role = Role.User, Content = userInput }
        };

        var request = new OllamaChatRequest
        {
            Model = _intentOllamaModel.Name,
            Messages = messages,
            Options = new Dictionary<string, object>
            {
                { "temperature", 0.0 },
                { "top_p", 0.1 }
            }
        };

        try
        {
            var response = await _ollamaApiService.SendChatAsync(request, cancellationToken);
            var jsonContent = response.Message.Content;

            var intention = JsonConvert.DeserializeObject<Intention>(jsonContent);

            return intention ?? CreateUnknownIntention();
        }
        catch (JsonException)
        {
            return CreateUnknownIntention();
        }
        catch (ApplicationException)
        {
            return CreateUnknownIntention();
        }
    }

    private static Intention CreateUnknownIntention()
    {
        return new Intention
        {
            Intent = "unknown",
            Entities = new Dictionary<string, string>()
        };
    }
}

