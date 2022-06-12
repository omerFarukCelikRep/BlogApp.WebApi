using BlogApp.Core.DataAccess.Interfaces;
using BlogApp.Entities.Concrete;

namespace BlogApp.DataAccess.Interfaces.Repositories;
public interface IRefreshTokenRepository : IInsertableRepositoryAsync<RefreshToken>, IUpdateableRepositoryAsync<RefreshToken>, IQueryableRepositoryAsync<RefreshToken>, IFindableRepositoryAsync<RefreshToken>, IRepositoryAsync
{
    Task<RefreshToken?> GetByRefreshTokenAsync(string refreshToken);
    Task<bool> UpdateRefreshTokenAsUsedAsync(RefreshToken refreshToken);
}
