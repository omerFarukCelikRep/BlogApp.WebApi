using BlogApp.Core.Entities.Base;
using System.Linq.Expressions;

namespace BlogApp.Core.DataAccess.Interfaces.Repositories;
public interface IAsyncOrderableRepository<TEntity> : IAsyncRepository where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true);
    Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, int takeCount = 0, bool tracking = true);
    Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true);
    Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, int takeCount = 0, bool tracking = true);
}
