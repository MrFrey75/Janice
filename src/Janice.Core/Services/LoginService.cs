namespace Janice.Core.Services;

using Janice.Core.Models;
using Janice.Core.Services.Interfaces;
using System.Threading.Tasks;


public class LoginService : ILoginService
{
    private readonly IUserService _userService;

    public LoginService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<User?> LoginAsync(string username, string password)
    {
        // Load all users and find by username
        var users = await _userService.GetAllUsersAsync();
        var user = users?.Find(u => u.UserName == username);
        if (user != null && user.VerifyPassword(password))
        {
            user.LastLogin = DateTime.UtcNow;
            await _userService.UpdateUserAsync(user);
            return user;
        }
        return null;
    }
}
