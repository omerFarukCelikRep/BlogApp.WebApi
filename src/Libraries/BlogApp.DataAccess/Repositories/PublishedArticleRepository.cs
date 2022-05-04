using BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
using BlogApp.DataAccess.Contexts;
using BlogApp.Entities.Concrete;
using Microsoft.Extensions.Logging;

namespace BlogApp.DataAccess.Repositories;
public class PublishedArticleRepository : EfBaseRepository<PublishedArticle, BlogAppDbContext>
{
    public PublishedArticleRepository(BlogAppDbContext context, ILogger<PublishedArticleRepository> logger) : base(context, logger) { }
}
