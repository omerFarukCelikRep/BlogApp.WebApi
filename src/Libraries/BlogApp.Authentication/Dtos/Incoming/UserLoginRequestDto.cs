namespace BlogApp.Authentication.Dtos.Incoming;
public record UserLoginRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; }
}
