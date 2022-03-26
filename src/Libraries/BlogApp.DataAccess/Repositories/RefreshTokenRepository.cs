using BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Contexts;
using BlogApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataAccess.Repositories;
public class RefreshTokenRepository : EfBaseRepository<RefreshToken, BlogAppDbContext>, IRefreshTokenRepository
{
    public RefreshTokenRepository(BlogAppDbContext context) : base(context) { }

    public async Task<RefreshToken?> GetByRefreshTokenAsync(string refreshToken)
    {
        try
        {
            return await _table.Where(x => x.Token.ToLower() == refreshToken.ToLower())
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            //TODO: Add logger
            return null;
        }
    }

    public async Task<bool> UpdateRefreshTokenAsUsed(RefreshToken refreshToken)
    {
        try
        {
            var token = await _table.Where(x => x.Token.ToLower() == refreshToken.Token.ToLower())
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync();

            if (token == null) return false;

            token.IsUsed = true;

            _ = await UpdateAsync(token);
            return true;
        }
        catch (Exception)
        {
            //TODO:Add Logger
            //TODO: Throw
            return false;
        }
    }
}
