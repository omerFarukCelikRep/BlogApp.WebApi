using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Entities.DbSets;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Authentication.Interfaces.Services;
public interface ITokenService
{
    string GenerateJwtToken(IdentityUser<Guid> identityUser, Guid userId);
    Task<RefreshToken> GenerateRefreshTokenAsync(IdentityUser<Guid> user, string ipAddress);
    Task<RefreshToken?> GetActiveRefreshTokenAsync(IdentityUser<Guid> user);
    Task<bool> UpdateRefreshTokenAsUsedAsync(string token);
    Task<Guid?> ValidateJwtTokenAsync(string token);
    Task<AuthResult> VerifyTokenAsync(TokenRequestDto tokenRequestDto);
}
