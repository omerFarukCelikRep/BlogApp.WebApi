namespace BlogApp.DataAccess.EFCore.Repositories;
public class ArticleRepository : EfBaseRepository<Article>, IArticleRepository
{
    public ArticleRepository(BlogAppDbContext context) : base(context) { }
}
