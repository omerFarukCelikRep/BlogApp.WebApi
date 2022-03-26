using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Authentication.Services.Abstract;
public interface ITokenService
{
    Task<AuthResult> GenerateJwtToken(IdentityUser<Guid> user);
    Task<AuthResult> UpdateToken(string refreshToken);
    Task<AuthResult?> VerifyToken(TokenRequestDto tokenRequestDto);
}
