namespace BlogApp.DataAccess.EFCore.Repositories;
public class ArticleRepository : EFBaseRepository<Article>, IArticleRepository
{
    public ArticleRepository(BlogAppDbContext context) : base(context) { }
}
