namespace Janice.Core.Services.Interfaces;

using Janice.Core.Models;


public interface ILoginService
{
    Task<User?> LoginAsync(string username, string password);
}