namespace BlogApp.DataAccess.EFCore.Repositories;
public class PublishedArticleRepository : EFBaseRepository<PublishedArticle>, IPublishedArticleRepository
{
    public PublishedArticleRepository(BlogAppDbContext context) : base(context) { }
}
