using BlogApp.Authentication.Dtos.Generic;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Authentication.Services.Abstract;
public interface ITokenService
{
    Task<TokenData> GenerateJwtToken(IdentityUser<Guid> user);
}
