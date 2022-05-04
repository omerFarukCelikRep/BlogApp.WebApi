using BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Contexts;
using BlogApp.Entities.Concrete;
using Microsoft.Extensions.Logging;

namespace BlogApp.DataAccess.EFCore.Repositories;
public class TopicRepository : EfBaseRepository<Topic, BlogAppDbContext>, ITopicRepository
{
    public TopicRepository(BlogAppDbContext context, ILogger<TopicRepository> logger) : base(context, logger) { }
}
