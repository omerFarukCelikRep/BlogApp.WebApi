using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataAccess.EFCore.Repositories;
public class RefreshTokenRepository : EFBaseRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(BlogAppDbContext context) : base(context) { }

    public async Task<RefreshToken?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        return await _table.Where(x => x.Token == refreshToken)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> UpdateRefreshTokenAsUsedAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        var token = await _table.Where(x => x.Token == refreshToken.Token)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(cancellationToken);

        if (token == null) return false;

        token.RevokedDate = DateTime.Now;

        await UpdateAsync(token, cancellationToken);

        return true;
    }
}
