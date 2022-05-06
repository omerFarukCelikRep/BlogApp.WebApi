namespace BlogApp.Authentication.Constants;
public class AuthenticationMessages
{
    public const string InvalidRequest = "Invalid authentication request";

    public const string JWTTokenNotExpired = "JWT token has not expired";
    public const string InvalidRefreshToken = "Invalid Refresh Token";
    public const string UsedRefreshToken = "Token has been used";
    public const string RevokedRefreshToken = "Refresh Token has been revoked, it cannot be used";

    public const string EmailAlredyTaken = "Email already taken";
}
