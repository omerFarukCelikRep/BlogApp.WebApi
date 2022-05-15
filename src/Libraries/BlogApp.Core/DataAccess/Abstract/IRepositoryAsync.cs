using BlogApp.Core.Entities.Base;
using System.Linq.Expressions;

namespace BlogApp.Core.DataAccess.Abstract;
public interface IRepositoryAsync<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true);
    Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true);
    Task<TEntity> GetByIdAsync(Guid id, bool tracking = true);
    Task<TEntity> AddAsync(TEntity entity);
    Task<bool> DeleteAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
}
