using BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
using BlogApp.DataAccess.Contexts;
using BlogApp.DataAccess.Interfaces;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.Concrete;
using Microsoft.Extensions.Logging;

namespace BlogApp.DataAccess.EFCore.Repositories;
public class PublishedArticleRepository : EfBaseRepository<PublishedArticle>, IPublishedArticleRepository
{
    public PublishedArticleRepository(BlogAppDbContext context, ILogger<PublishedArticleRepository> logger) : base(context, logger) { }
}
