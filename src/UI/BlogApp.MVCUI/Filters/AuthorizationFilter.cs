using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlogApp.MVCUI.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizationFilter : Attribute, IAsyncAuthorizationFilter
{
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Session.GetString("Token");
        if (string.IsNullOrEmpty(token))
        {
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new { controller = "Home", action = "Login" })
            );
        }
        else
        {
            JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            context.HttpContext.User = new(new ClaimsIdentity(jwtSecurityToken.Claims, "JwtAuthType"));
        }

        return Task.CompletedTask;
    }
}
