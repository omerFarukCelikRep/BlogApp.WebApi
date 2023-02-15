namespace BlogApp.DataAccess.EFCore.Repositories;
public class TopicRepository : EFBaseRepository<Topic>, ITopicRepository
{
    public TopicRepository(BlogAppDbContext context) : base(context) { }
}
