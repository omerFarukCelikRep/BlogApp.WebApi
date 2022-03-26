using BlogApp.Core.DataAccess.Abstract;
using BlogApp.Entities.Concrete;

namespace BlogApp.DataAccess.Abstract;
public interface IRefreshTokenRepository : IRepositoryAsync<RefreshToken>
{
    Task<RefreshToken?> GetByRefreshTokenAsync(string refreshToken);
    Task<bool> UpdateRefreshTokenAsUsed(RefreshToken refreshToken);
}
