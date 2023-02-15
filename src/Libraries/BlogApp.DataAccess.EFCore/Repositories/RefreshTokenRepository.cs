using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataAccess.EFCore.Repositories;
public class RefreshTokenRepository : EFBaseRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(BlogAppDbContext context) : base(context) { }

    public async Task<RefreshToken?> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _table.Where(x => x.Token == refreshToken)
                            .AsNoTracking()
                            .FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateRefreshTokenAsUsedAsync(RefreshToken refreshToken)
    {
        var token = await _table.Where(x => x.Token == refreshToken.Token)
                                .AsNoTracking()
                                .FirstOrDefaultAsync();

        if (token == null) return false;

        token.RevokedDate = DateTime.Now;

        _ = await UpdateAsync(token);

        return true;
    }
}
