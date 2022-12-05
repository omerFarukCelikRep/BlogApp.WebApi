namespace BlogApp.Authentication.Configurations;

public class JwtOptions
{
    public string Audience { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public string Secret { get; init; } = null!;
    public TimeSpan ExpiryTimeFrame { get; init; }
}