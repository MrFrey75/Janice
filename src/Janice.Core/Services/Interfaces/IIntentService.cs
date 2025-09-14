using Janet.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Janet.Core.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for a service that determines the user's intent from their input.
    /// </summary>
    public interface IIntentService
    {
        /// <summary>
        /// Initializes the service by selecting an appropriate model for intent recognition.
        /// </summary>
        Task InitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Analyzes the user's input string to determine their intent and extract relevant entities.
        /// </summary>
        /// <param name="userInput">The raw text input from the user.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>An <see cref="Intention"/> object representing the parsed intent and entities.</returns>
        Task<Intention> GetIntentionAsync(string userInput, CancellationToken cancellationToken = default);
    }
}

