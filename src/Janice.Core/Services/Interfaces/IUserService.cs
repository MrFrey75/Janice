using Janice.Core.Models;

namespace Janice.Core.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for a service that manages user-related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves all users in the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of users.</returns>
        Task<List<User>> GetAllUsersAsync();
        /// <summary>
        /// Retrieves user information by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user information.</returns>
    Task<User?> GetUserByIdAsync(Guid userId);

        /// <summary>
        /// Creates a new user in the system.
        /// </summary>
        /// <param name="user">The user information to create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created user information.</returns>
    Task<User> CreateUserAsync(User user);

        /// <summary>
        /// Updates existing user information.
        /// </summary>
        /// <param name="user">The updated user information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated user information.</returns>
    Task<User?> UpdateUserAsync(User user);

        /// <summary>
        /// Deletes a user from the system by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteUserAsync(Guid userId);
    }
    }