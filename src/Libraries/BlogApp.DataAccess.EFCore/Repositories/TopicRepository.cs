using BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
using BlogApp.DataAccess.Contexts;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.Concrete;
using Microsoft.Extensions.Logging;

namespace BlogApp.DataAccess.EFCore.Repositories;
public class TopicRepository : EfBaseRepository<Topic>, ITopicRepository
{
    public TopicRepository(BlogAppDbContext context, ILogger<TopicRepository> logger) : base(context, logger) { }
}
