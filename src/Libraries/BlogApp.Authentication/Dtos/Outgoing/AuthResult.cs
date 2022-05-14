namespace BlogApp.Authentication.Dtos.Outgoing;
public class AuthResult
{
    public AuthResult() { }

    public AuthResult(bool success, params string[] errors)
    {
        Token = string.Empty;
        RefreshToken = string.Empty;
        Success = success;
        Errors.AddRange(errors);
    }

    public AuthResult(string token, string refreshToken, bool success)
    {
        Token = token;
        RefreshToken = refreshToken;
        Success = success;
    }

    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public bool Success { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
}
