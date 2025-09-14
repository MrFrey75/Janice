namespace Janet.Core.Models;

public class User
{
    public Guid Uid { get; set; } = Guid.NewGuid();
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastLogin { get; set; } = DateTime.UtcNow;

    // Secure password storage (hashed)
    private string _passwordHash = string.Empty;

    public void SetPassword(string password)
    {
        // Use a secure hash algorithm (e.g., BCrypt, PBKDF2, or Argon2)
        // For demonstration, using SHA256 (replace with a stronger hash in production)
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        _passwordHash = Convert.ToBase64String(hash);
    }

    public bool VerifyPassword(string password)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        var hashString = Convert.ToBase64String(hash);
        return _passwordHash == hashString;
    }
    // Additional properties can be added as needed
    
}