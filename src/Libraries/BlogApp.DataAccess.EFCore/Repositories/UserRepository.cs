namespace BlogApp.DataAccess.EFCore.Repositories;
public class UserRepository : EFBaseRepository<User>, IUserRepository
{
    public UserRepository(BlogAppDbContext context) : base(context) { }

    public Task<User?> GetByEmailAsync(string email)
    {
        return GetAsync(x => x.Email == email);
    }
}
