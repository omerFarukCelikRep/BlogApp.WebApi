using System.IdentityModel.Tokens.Jwt;

namespace BlogApp.Core.Utilities.Authentication;
public class JwtHelper
{
    public static string? GetUserIdByToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var decodedToken = tokenHandler.ReadJwtToken(token);

        return decodedToken.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
    }
}
