using BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
using BlogApp.DataAccess.Contexts;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.Concrete;

namespace BlogApp.DataAccess.EFCore.Repositories;
public class ArticleRepository : EfBaseRepository<Article>, IArticleRepository
{
    public ArticleRepository(BlogAppDbContext context) : base(context) { }
}
