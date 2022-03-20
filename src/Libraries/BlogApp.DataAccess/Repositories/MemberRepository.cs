using BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Contexts;
using BlogApp.Entities.Concrete;

namespace BlogApp.DataAccess.Repositories;
public class MemberRepository : EfBaseRepository<Member, BlogAppDbContext>, IMemberRepository
{
    public MemberRepository(BlogAppDbContext context) : base(context) { }
}
