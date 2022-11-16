using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace BlogApp.MVCUI.Handlers.Authentication;

public class BlogAppAuthenticationHandler : AuthenticationHandler<BlogAppAuthenticationSchemeOptions>
{
    public BlogAppAuthenticationHandler(IOptionsMonitor<BlogAppAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            var token = Context.Session?.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
            }

            JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            var identity = new ClaimsIdentity(jwtSecurityToken.Claims, Scheme.Name);
            var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch (Exception ex)
        {
            return Task.FromResult(AuthenticateResult.Fail(ex.Message));
        }
    }
}
