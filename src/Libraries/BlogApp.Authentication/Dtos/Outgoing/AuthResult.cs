namespace BlogApp.Authentication.Dtos.Outgoing;
public class AuthResult
{
    public AuthResult() { }
    
    public AuthResult(string token, string refreshToken, bool success, params string[] errors)
    {
        Token = token;
        RefreshToken = refreshToken;
        Success = success;

        foreach (var error in errors)
        {
            Errors.Add(error);
        }
    }

    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public bool Success { get; set; }
    public List<string> Errors { get; set; }
}
