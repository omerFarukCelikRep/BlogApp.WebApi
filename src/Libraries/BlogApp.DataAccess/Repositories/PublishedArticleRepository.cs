using BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
using BlogApp.DataAccess.Contexts;
using BlogApp.Entities.Concrete;

namespace BlogApp.DataAccess.Repositories;
public class PublishedArticleRepository : EfBaseRepository<PublishedArticle, BlogAppDbContext>
{
    public PublishedArticleRepository(BlogAppDbContext context) : base(context) { }
}
