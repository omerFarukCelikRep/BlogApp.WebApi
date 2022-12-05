using BlogApp.Authentication.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Authentication.Middlewares;
public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ITokenService tokenService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrEmpty(token))
        {
            await _next(context);
        }

        var userId = await tokenService.ValidateJwtTokenAsync(token!);

        if (userId is null || userId == Guid.Empty)
        {
            context.Items["User"] = userId;
        }

        await _next(context);
    }
}
