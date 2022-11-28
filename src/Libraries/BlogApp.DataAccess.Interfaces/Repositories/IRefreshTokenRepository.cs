using BlogApp.Core.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.DbSets;

namespace BlogApp.DataAccess.Interfaces.Repositories;
public interface IRefreshTokenRepository : IAsyncInsertableRepository<RefreshToken>, IAsyncUpdateableRepository<RefreshToken>, IAsyncQueryableRepository<RefreshToken>, IAsyncFindableRepository<RefreshToken>, IAsyncRepository
{
    Task<RefreshToken?> GetByRefreshTokenAsync(string refreshToken);
    Task<bool> UpdateRefreshTokenAsUsedAsync(RefreshToken refreshToken);
}
