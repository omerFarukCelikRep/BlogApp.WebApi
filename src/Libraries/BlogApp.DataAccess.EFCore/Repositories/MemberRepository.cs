using BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
using BlogApp.DataAccess.Contexts;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.DbSets;

namespace BlogApp.DataAccess.EFCore.Repositories;
public class MemberRepository : EfBaseRepository<User>, IUserRepository
{
    public MemberRepository(BlogAppDbContext context) : base(context) { }

    public async Task<User> GetByIdentityId(Guid identityId)
    {
        return await GetAsync(x => x.IdentityId == identityId);
    }
}
