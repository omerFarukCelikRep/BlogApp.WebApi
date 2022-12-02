namespace BlogApp.DataAccess.Interfaces.Repositories;
public interface IUserRepository : IAsyncInsertableRepository<User>, IAsyncUpdateableRepository<User>, IAsyncQueryableRepository<User>, IAsyncFindableRepository<User>, IAsyncRepository
{
    Task<User?> GetByIdentityId(Guid identityId);
}
