namespace BlogApp.DataAccess.EFCore.Repositories;
public class CommentRepository : EFBaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(BlogAppDbContext context) : base(context) { }
}
