using BlogApp.Core.Entities.Base;
using System.Linq.Expressions;

namespace BlogApp.Core.DataAccess.Interfaces.Repositories;
public interface IAsyncFindableRepository<TEntity> : IAsyncRepository where TEntity : BaseEntity
{
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(Guid id, bool tracking = true, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null, CancellationToken cancellationToken = default);
}
