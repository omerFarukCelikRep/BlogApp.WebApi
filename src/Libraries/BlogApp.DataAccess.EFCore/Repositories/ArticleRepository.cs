using BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
using BlogApp.DataAccess.Contexts;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.Concrete;
using Microsoft.Extensions.Logging;

namespace BlogApp.DataAccess.EFCore.Repositories;
public class ArticleRepository : EfBaseRepository<Article>, IArticleRepository
{
    public ArticleRepository(BlogAppDbContext context, ILogger<ArticleRepository> logger) : base(context, logger) { }
}
