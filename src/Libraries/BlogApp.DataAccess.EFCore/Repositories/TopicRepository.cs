namespace BlogApp.DataAccess.EFCore.Repositories;
public class TopicRepository : EfBaseRepository<Topic>, ITopicRepository
{
    public TopicRepository(BlogAppDbContext context) : base(context) { }
}
