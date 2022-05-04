using BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Contexts;
using BlogApp.Entities.Concrete;
using Microsoft.Extensions.Logging;

namespace BlogApp.DataAccess.Repositories;
public class ArticleRepository : EfBaseRepository<Article, BlogAppDbContext>, IArticleRepository
{
    public ArticleRepository(BlogAppDbContext context, ILogger<ArticleRepository> logger) : base(context, logger) { }
}
