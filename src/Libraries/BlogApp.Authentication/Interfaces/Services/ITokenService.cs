using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Entities.DbSets;

namespace BlogApp.Authentication.Interfaces.Services;
public interface ITokenService
{
    Task<string> GenerateJwtToken(User user, bool rememberMe = false, CancellationToken cancellationToken = default);
    Task<RefreshToken> GenerateRefreshTokenAsync(User user, string ipAddress, CancellationToken cancellationToken = default);
    Task<RefreshToken?> GetActiveRefreshTokenAsync(User user, CancellationToken cancellationToken = default);
    Task<bool> UpdateRefreshTokenAsUsedAsync(string token, CancellationToken cancellationToken = default);
    Task<Guid?> ValidateJwtTokenAsync(string token, CancellationToken cancellationToken = default);
    Task<AuthResult> VerifyTokenAsync(TokenRequestDto tokenRequestDto, CancellationToken cancellationToken = default);
}
