using BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
using BlogApp.DataAccess.Contexts;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.Concrete;

namespace BlogApp.DataAccess.EFCore.Repositories;
public class TopicRepository : EfBaseRepository<Topic>, ITopicRepository
{
    public TopicRepository(BlogAppDbContext context) : base(context) { }
}
