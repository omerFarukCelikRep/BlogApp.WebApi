using BlogApp.Authentication.Interfaces.Providers;
using BlogApp.Authentication.Interfaces.Services;
using BlogApp.Authentication.Options;
using BlogApp.Authentication.Providers;
using BlogApp.Authentication.Services;
using BlogApp.Core.Utilities.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Authentication.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddAuthentication()
        .AddJwtBearer();

        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddSingleton<JwtHelper>();

        return services;
    }
}
