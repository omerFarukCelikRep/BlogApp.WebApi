namespace BlogApp.Authentication.Dtos.Generic;
public class TokenData
{
    public string JwtToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}
