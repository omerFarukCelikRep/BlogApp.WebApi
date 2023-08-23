using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataAccess.EFCore.Repositories;
public class UserSessionRepository : EFBaseRepository<UserSession>, IUserSessionRepository
{
    public UserSessionRepository(DbContext context) : base(context) { }
}
