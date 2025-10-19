namespace Shopify.UsersAPI.Models;

public class UserDTO
{
    public UserDTO(Guid id, string email, DateTimeOffset createdAt, string jwt)
    {
        Id = id;
        Email = email;
        CreatedAt = createdAt;
        JWT = jwt;
    }
    public UserDTO(User user, string jwt)
    {
        Id = user.Id;
        Email = user.Email;
        CreatedAt = user.CreatedAt;
        JWT = jwt;
    }
    public Guid Id { get; set; }
    public string Email { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string JWT { get; set; }
}