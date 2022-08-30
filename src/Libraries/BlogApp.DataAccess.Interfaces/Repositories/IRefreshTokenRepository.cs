using BlogApp.Core.DataAccess.Interfaces;
using BlogApp.Entities.Concrete;

namespace BlogApp.DataAccess.Interfaces.Repositories;
public interface IRefreshTokenRepository : IAsyncInsertableRepository<RefreshToken>, IAsyncUpdateableRepository<RefreshToken>, IAsyncQueryableRepository<RefreshToken>, IAsyncFindableRepository<RefreshToken>, IAsyncRepository
{
    Task<RefreshToken?> GetByRefreshTokenAsync(string refreshToken);
    Task<bool> UpdateRefreshTokenAsUsedAsync(RefreshToken refreshToken);
}
