using CommonExtensions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shopify.UsersAPI.Services;

public class JWTService(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;
    public string GenerateToken(Guid userId)
    {
        var secret = _configuration.GetRequiredValue("JWT:secret");
        var expiresMinutes = int.Parse(_configuration.GetRequiredValue("JWT:expiresMinutes"));
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: "shopmate.auth",
            audience: "shopmate.api",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
