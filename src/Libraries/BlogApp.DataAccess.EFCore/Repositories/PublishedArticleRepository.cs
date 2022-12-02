namespace BlogApp.DataAccess.EFCore.Repositories;
public class PublishedArticleRepository : EfBaseRepository<PublishedArticle>, IPublishedArticleRepository
{
    public PublishedArticleRepository(BlogAppDbContext context) : base(context) { }
}
