using BlogApp.Core.Entities.Base;

namespace BlogApp.Core.DataAccess.Interfaces.Repositories;
public interface IAsyncInsertableRepository<TEntity> : IAsyncRepository where TEntity : BaseEntity
{
    Task<TEntity> AddAsync(TEntity entity);
}
