using BlogApp.Core.DataAccess.Interfaces;
using BlogApp.Entities.DbSets;

namespace BlogApp.DataAccess.Interfaces.Repositories;
public interface IMemberRepository : IAsyncInsertableRepository<Member>, IAsyncUpdateableRepository<Member>, IAsyncQueryableRepository<Member>, IAsyncFindableRepository<Member>, IAsyncRepository
{
    Task<Member> GetByIdentityId(Guid identityId);
}
