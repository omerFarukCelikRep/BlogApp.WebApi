using System.IdentityModel.Tokens.Jwt;

namespace BlogApp.Core.Utilities.Authentication;
public class JwtHelper
{
    private const string Id = "Id";
    private static readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
    public static string? GetUserIdByToken(string token)
    {
        var decodedToken = _jwtSecurityTokenHandler.ReadJwtToken(token);

        return decodedToken.Claims.FirstOrDefault(x => x.Type == Id)?.Value;
    }

    public static JwtSecurityToken? Read(string token)
    {
        return _jwtSecurityTokenHandler.ReadJwtToken(token);
    }
}
