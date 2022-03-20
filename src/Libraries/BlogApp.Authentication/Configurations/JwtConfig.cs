namespace BlogApp.Authentication.Configurations;

public class JwtConfig
{
    public string? Secret { get; set; }
    public TimeSpan ExpiryTimeFrame { get; set; }
}