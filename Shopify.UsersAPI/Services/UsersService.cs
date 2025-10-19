using Shopify.UsersAPI.Exceptions;
using Shopify.UsersAPI.Models;

namespace Shopify.UsersAPI.Services;

public class UsersService(JWTService jWTService)
{
    private List<User> _users = [];
    private JWTService _jWTService = jWTService;

    public async Task<UserDTO> CreateUser(string email, string password)
    {
        if(
            _users.Any(x => x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase))
            )
        {
            throw new BadHttpRequestException("User already exists");
        }
        
        var (hash, salt) = PasswordHasherService.HashPassword(password);
        var newUser = new User(Guid.NewGuid(), email, hash, salt);
        _users.Add(newUser);
        var jwt = _jWTService.GenerateToken(newUser.Id);
        return new UserDTO(newUser.Id, newUser.Email, newUser.CreatedAt, jwt);
    }

    public async Task<UserDTO> Login(string email, string password)
    {
        var user = _users.FirstOrDefault(x => x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
        if(user == null)
        {
            throw new AuthorizationException();
        }
        if (!PasswordHasherService.VerifyPassword(password, user.PasswordHash, user.Salt))
        {
            throw new AuthorizationException();
        }

        return new UserDTO(user, _jWTService.GenerateToken(user.Id));
    }
}
