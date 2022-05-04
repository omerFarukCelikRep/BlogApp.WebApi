namespace BlogApp.Authentication.Dtos.Incoming;
public class TokenRequestDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public string IpAddress { get; set; }
}
