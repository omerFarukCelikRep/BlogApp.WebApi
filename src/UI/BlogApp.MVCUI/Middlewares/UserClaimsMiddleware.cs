using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlogApp.MVCUI.Middlewares;

public class UserClaimsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserClaimsMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
    {
        _next = next;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        string token = _httpContextAccessor.HttpContext.Session.GetString("Token");
        if (!string.IsNullOrEmpty(token))
        {
            JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            httpContext.User = new(new ClaimsIdentity(jwtSecurityToken.Claims, "JwtAuthType"));

        }

        await _next(httpContext);
    }
}
