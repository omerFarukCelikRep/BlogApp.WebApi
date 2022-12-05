using Microsoft.AspNetCore.Identity;

namespace BlogApp.Authentication.Interfaces.Providers;
public interface IJwtProvider
{
    string Generate(IdentityUser<Guid> identityUser, Guid userId);
}
