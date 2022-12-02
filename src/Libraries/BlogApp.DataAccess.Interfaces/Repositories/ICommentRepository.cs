namespace BlogApp.DataAccess.Interfaces.Repositories;
public interface ICommentRepository : IAsyncRepository, IAsyncInsertableRepository<Comment>, IAsyncQueryableRepository<Comment>
{
}
