using BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Contexts;
using BlogApp.Entities.Concrete;

namespace BlogApp.DataAccess.Repositories;
public class RefreshTokenRepository : EfBaseRepository<RefreshToken, BlogAppDbContext>, IRefreshTokenRepository
{
    public RefreshTokenRepository(BlogAppDbContext context) : base(context) { }
}
