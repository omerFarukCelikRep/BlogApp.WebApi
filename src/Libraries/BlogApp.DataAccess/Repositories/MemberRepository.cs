using BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Contexts;
using BlogApp.Entities.Concrete;
using Microsoft.Extensions.Logging;

namespace BlogApp.DataAccess.Repositories;
public class MemberRepository : EfBaseRepository<Member, BlogAppDbContext>, IMemberRepository
{
    public MemberRepository(BlogAppDbContext context, ILogger<MemberRepository> logger) : base(context, logger) { }

    public async Task<Member> GetByIdentityId(Guid identityId)
    {
        return await GetAsync(x => x.IdentityId == identityId);
    }
}
