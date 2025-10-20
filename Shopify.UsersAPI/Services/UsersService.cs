using Shopify.CommonExceptions;
using Shopify.UsersAPI.Models;
using Shopify.UsersAPI.RequestBodies;

namespace Shopify.UsersAPI.Services;

public class UsersService(JWTService jWTService)
{
    private List<User> _users = [];
    private JWTService _jWTService = jWTService;

    public async Task<UserDTO> CreateUser(UserRequestBody userRequest, CancellationToken cancellationToken)
    {
        if(
            _users.Any(x => x.Email.Equals(userRequest.Email, StringComparison.InvariantCultureIgnoreCase))
            )
        {
            throw new BadHttpRequestException("User already exists");
        }
        
        var (hash, salt) = PasswordHasherService.HashPassword(userRequest.Password);
        var newUser = new User(Guid.NewGuid(), userRequest.Password, hash, salt);
        _users.Add(newUser);
        var jwt = _jWTService.GenerateToken(newUser.Id);
        return new UserDTO(newUser.Id, newUser.Email, newUser.CreatedAt, jwt);
    }

    public async Task<UserDTO> Login(UserRequestBody userRequest, CancellationToken cancellationToken)
    {
        var user = _users.FirstOrDefault(x => x.Email.Equals(userRequest.Email, StringComparison.InvariantCultureIgnoreCase));
        if(user == null)
        {
            throw new AuthorizationException();
        }
        if (!PasswordHasherService.VerifyPassword(userRequest.Password, user.PasswordHash, user.Salt))
        {
            throw new AuthorizationException();
        }

        return new UserDTO(user, _jWTService.GenerateToken(user.Id));
    }
}
