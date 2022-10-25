using BlogApp.Core.DataAccess.Interfaces;
using BlogApp.Entities.DbSets;

namespace BlogApp.DataAccess.Interfaces.Repositories;
public interface ICommentRepository : IAsyncRepository, IAsyncInsertableRepository<Comment>, IAsyncQueryableRepository<Comment>
{
}
