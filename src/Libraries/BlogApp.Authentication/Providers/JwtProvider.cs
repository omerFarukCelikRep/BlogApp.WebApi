using BlogApp.Authentication.Configurations;
using BlogApp.Authentication.Interfaces.Providers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogApp.Authentication.Providers;
public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    public JwtProvider(IOptions<JwtOptions> options)
    {
        _jwtOptions = options.Value;
    }

    public string Generate(IdentityUser<Guid> identityUser, Guid userId)
    {
        var claims = new Claim[]
        {
            new (ClaimTypes.NameIdentifier, identityUser.Id.ToString()),
            new (JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Email, identityUser.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) //Used by the refreshed token
        };

        var signInCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret!)), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_jwtOptions.Issuer, _jwtOptions.Audience, claims, null, DateTime.Now.Add(_jwtOptions.ExpiryTimeFrame), signInCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
