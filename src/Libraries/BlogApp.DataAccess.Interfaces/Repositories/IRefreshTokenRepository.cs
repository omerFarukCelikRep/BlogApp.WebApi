namespace BlogApp.DataAccess.Interfaces.Repositories;
public interface IRefreshTokenRepository : IAsyncInsertableRepository<RefreshToken>, IAsyncUpdateableRepository<RefreshToken>, IAsyncQueryableRepository<RefreshToken>, IAsyncFindableRepository<RefreshToken>, IAsyncRepository
{
    Task<RefreshToken?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task<bool> UpdateRefreshTokenAsUsedAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
}
