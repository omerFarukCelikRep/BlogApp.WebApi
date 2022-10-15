using BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
using BlogApp.DataAccess.Contexts;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.DbSets;

namespace BlogApp.DataAccess.EFCore.Repositories;
public class PublishedArticleRepository : EfBaseRepository<PublishedArticle>, IPublishedArticleRepository
{
    public PublishedArticleRepository(BlogAppDbContext context) : base(context) { }
}
