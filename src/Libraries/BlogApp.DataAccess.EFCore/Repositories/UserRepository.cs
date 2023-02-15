namespace BlogApp.DataAccess.EFCore.Repositories;
public class UserRepository : EFBaseRepository<User>, IUserRepository
{
    public UserRepository(BlogAppDbContext context) : base(context) { }

    public Task<User?> GetByIdentityId(Guid identityId)
    {
        return GetAsync(x => x.IdentityId == identityId);
    }
}
