using BlogApp.Authentication.Configurations;
using BlogApp.Authentication.Interfaces.Services;
using BlogApp.Authentication.Services;
using BlogApp.Core.Utilities.Authentication;
using BlogApp.Core.Utilities.Configurations;
using BlogApp.DataAccess.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlogApp.Authentication.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
    {
        services.Configure<JwtConfig>(Configuration.GetSection("Jwt"));

        var secret = Configuration.GetValue("Jwt:Secret");

        var key = Encoding.ASCII.GetBytes(secret!);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false, //TODO:Update this later
            ValidateAudience = false, //TODO:Update this later
            RequireExpirationTime = false, //TODO: Update this later
            ValidateLifetime = true
        };

        services.AddSingleton(tokenValidationParameters);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = tokenValidationParameters;
        });

        //services.AddAuthentication("Bearer").AddJwtBearer();

        services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedAccount = true;
        })
            .AddEntityFrameworkStores<BlogAppDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<ITokenService, TokenService>();
        services.AddSingleton<JwtHelper>();

        return services;
    }
}
