using System.IdentityModel.Tokens.Jwt;

namespace BlogApp.Core.Utilities.Authentication;
public class JwtHelper
{
    public static Guid GetUserIdByToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var decodedToken = tokenHandler.ReadJwtToken(token);
        return Guid.Parse(decodedToken.Claims.FirstOrDefault(x => x.Type == "Id")?.Value);
    }
}
