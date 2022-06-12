using BlogApp.Core.Entities.Base;

namespace BlogApp.Core.DataAccess.Interfaces;
public interface IInsertableRepositoryAsync<TEntity> : IRepositoryAsync where TEntity : BaseEntity
{
    Task<TEntity> AddAsync(TEntity entity);
}
