using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Shared.Web.Security.Services;

public sealed class TokenService :
    ITokenService, 
    IScopedService
{
    private readonly IConfigService configService;

    public TokenService(
        IConfigService configService)
        => this.configService = configService;

    public string GetToken(
        User user)
    {
        var tokenDescriptor = GetTokenDescriptor(user);

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        var token = tokenHandler.WriteToken(securityToken);

        return token;
    }

    private SecurityTokenDescriptor GetTokenDescriptor(
        User user)
    {
        var expiringAfter = configService.JwtExpireAfterMinutes;

        var encryptionKey = configService.JwtEncryptionKey;

        var securityKey = Encoding.UTF8.GetBytes(encryptionKey);

        var symmetricSecurityKey = new SymmetricSecurityKey(securityKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(user.Claims()) ,

            Expires = DateTime.UtcNow.AddMinutes(expiringAfter) ,

            SigningCredentials = new SigningCredentials(
                symmetricSecurityKey ,
                SecurityAlgorithms.HmacSha256Signature)
        };

        return tokenDescriptor;
    }
}