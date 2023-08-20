namespace BlogApp.Authentication.Dtos.Outgoing;
public class AuthResult
{
    private List<string> _errors = new();
    public AuthResult() { }

    public AuthResult(bool success, params string[] errors)
    {
        Success = success;
        _errors = new (errors);
    }

    public AuthResult(bool success,string token, string refreshToken)
    {
        Success = success;
        Token = token;
        RefreshToken = refreshToken;
    }

    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public bool Success { get; set; }
    public IReadOnlyList<string> Errors => _errors.AsReadOnly();
}