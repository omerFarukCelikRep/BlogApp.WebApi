using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Entities.Concrete;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Authentication.Services.Abstract;
public interface ITokenService
{
    string GenerateJwtToken(IdentityUser<Guid> user);
    Task<RefreshToken> GenerateRefreshTokenAsync(IdentityUser<Guid> user, string ipAddress);
    Task<RefreshToken> GetActiveRefreshTokenAsync(IdentityUser<Guid> user);
    Task<bool> UpdateRefreshTokenAsUsedAsync(string token);
    Task<Guid?> ValidateJwtTokenAsync(string token);
    Task<AuthResult?> VerifyTokenAsync(TokenRequestDto tokenRequestDto);
}
