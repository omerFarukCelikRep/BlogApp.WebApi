using BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Contexts;
using BlogApp.Entities.Concrete;

namespace BlogApp.DataAccess.Repositories;
public class ArticleRepository : EfBaseRepository<Article, BlogAppDbContext>, IArticleRepository
{
    public ArticleRepository(BlogAppDbContext context) : base(context) { }
}
