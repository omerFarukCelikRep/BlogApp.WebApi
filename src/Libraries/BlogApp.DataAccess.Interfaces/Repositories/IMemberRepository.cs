using BlogApp.Core.DataAccess.Interfaces;
using BlogApp.Entities.Concrete;

namespace BlogApp.DataAccess.Interfaces.Repositories;
public interface IMemberRepository : IInsertableRepositoryAsync<Member>, IUpdateableRepositoryAsync<Member>, IQueryableRepositoryAsync<Member>, IFindableRepositoryAsync<Member>, IRepositoryAsync
{
    Task<Member> GetByIdentityId(Guid identityId);
}
