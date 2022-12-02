namespace BlogApp.DataAccess.EFCore.Repositories;
public class CommentRepository : EfBaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(BlogAppDbContext context) : base(context) { }
}
