using BlogApp.Core.Entities.Base;
using System.Linq.Expressions;

namespace BlogApp.Core.DataAccess.Interfaces;
public interface IQueryableRepositoryAsync<TEntity> : IRepositoryAsync where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true);
}
