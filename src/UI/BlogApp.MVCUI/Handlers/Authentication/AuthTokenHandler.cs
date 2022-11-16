using System.Net.Http.Headers;

namespace BlogApp.MVCUI.Handlers.Authentication;

public class AuthTokenHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthTokenHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Headers.Add("X-Forwarded-For", _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString());
            }
        }

        return base.SendAsync(request, cancellationToken);
    }
}
