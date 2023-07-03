namespace BlogApp.Authentication.Constants;
public struct AuthenticationMessages
{
    public const string InvalidRequest = "Invalid authentication request";

    public const string JWTTokenNotExpired = "JWT token has not expired";
    public const string InvalidRefreshToken = "Invalid Refresh Token";
    public const string UsedRefreshToken = "Token has been used";
    public const string RevokedRefreshToken = "Refresh Token has been revoked, it cannot be used";

    public const string EmailAlredyTaken = "Email already taken";

    public const string PasswordIsNotValid = """Please choose a valid password. At least one digit [0-9]. At least one lowercase character [a-z]. At least one uppercase character [A-Z]. At least one special character [*.!@#$%^&(){}[]:;<>,.?/~_+-=|\]. At least 8 characters in length, but no more than 32.""";
    public const string PasswordMustMatch = "Password must match";
}
