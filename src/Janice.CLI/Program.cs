using System;
using System.Linq;
using System.Threading.Tasks;
using Janice.Core.Services;

// This assumes the OllamaService class from the other file is available in the same project/namespace.
// If you place OllamaService in a namespace like "Janice.Core.Services", you would add:
// using Janice.Core.Services;

namespace Janice.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The OllamaService class implements IDisposable (because it has a Dispose method).
            // It's best practice to wrap it in a 'using' statement to ensure resources
            // like the HttpClient are properly released. This is the fix related to the
            // code you had selected.
            using (var ollamaService = new OllamaApiService())
            {
                Console.WriteLine("Fetching local Ollama models...");
                var models = await ollamaService.GetLocalModels();

                if (models != null && models.Any())
                {
                    Console.WriteLine("\nAvailable Models:");
                    foreach (var model in models)
                    {
                        // Convert size to GB for better readability
                        var sizeInGb = Math.Round(model.Size / 1e9, 2);
                        Console.WriteLine($"- {model.Name} (Size: {sizeInGb} GB)");
                    }

                    // --- Example of how to use GetResponse ---
                    Console.WriteLine("\n----------------------------------------");
                    // Select the first model for a test query
                    var testModel = models.First().Name;
                    Console.WriteLine($"To test, I'll use the '{testModel}' model.");
                    Console.Write("Enter a prompt (or press Enter to skip): ");
                    var prompt = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(prompt))
                    {
                        Console.WriteLine("\nGenerating response...");
                        var response = await ollamaService.GetResponse(testModel, prompt);
                        Console.WriteLine("\n--- Response from Ollama ---");
                        Console.WriteLine(response);
                        Console.WriteLine("----------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo local models found.");
                    Console.WriteLine("Please ensure the Ollama service is running and you have at least one model pulled (e.g., 'ollama run llama3').");
                }
            }

            Console.WriteLine("\nProgram finished. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
