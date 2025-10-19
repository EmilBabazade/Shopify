namespace Shopify.UsersAPI.Models;

public class User(Guid id, string email, string passwordHash, string salt)
{
    public Guid Id { get; set; } = id;
    public string Email { get; set; } = email;
    public string PasswordHash { get; set; } = passwordHash;
    public string Salt { get; set; } = salt;
    public DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;
}
