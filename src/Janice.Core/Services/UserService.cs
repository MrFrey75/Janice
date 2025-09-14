namespace Janice.Core.Services;

using Janice.Core.Models;
using Janice.Core.Services.Interfaces;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class UserService : IUserService
{
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await LoadUsersAsync();
    }
    // Implementation of user-related operations would go here.

    private readonly string _dataFile = Path.Combine("Data", "users.yaml");
    private readonly IDeserializer _deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
    private readonly ISerializer _serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

    private async Task<List<User>> LoadUsersAsync()
    {
        if (!File.Exists(_dataFile)) return new List<User>();
        var yaml = await File.ReadAllTextAsync(_dataFile);
        if (string.IsNullOrWhiteSpace(yaml)) return new List<User>();
        return _deserializer.Deserialize<List<User>>(yaml) ?? new List<User>();
    }

    private async Task SaveUsersAsync(List<User> users)
    {
        var yaml = _serializer.Serialize(users);
        await File.WriteAllTextAsync(_dataFile, yaml);
    }

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        var users = await LoadUsersAsync();
        return users.Find(u => u.Uid == userId);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        var users = await LoadUsersAsync();
        users.Add(user);
        await SaveUsersAsync(users);
        return user;
    }

    public async Task<User?> UpdateUserAsync(User user)
    {
        var users = await LoadUsersAsync();
        var idx = users.FindIndex(u => u.Uid == user.Uid);
        if (idx >= 0)
        {
            users[idx] = user;
            await SaveUsersAsync(users);
            return user;
        }
        return null;
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var users = await LoadUsersAsync();
        users.RemoveAll(u => u.Uid == userId);
        await SaveUsersAsync(users);
    }
}