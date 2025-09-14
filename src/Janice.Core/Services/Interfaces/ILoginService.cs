namespace Janet.Core.Services.Interfaces;

using Janet.Core.Models;


public interface ILoginService
{
    Task<User?> LoginAsync(string username, string password);
}