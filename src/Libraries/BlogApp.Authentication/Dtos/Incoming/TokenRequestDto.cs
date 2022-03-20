namespace BlogApp.Authentication.Dtos.Incoming;
public class TokenRequestDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
