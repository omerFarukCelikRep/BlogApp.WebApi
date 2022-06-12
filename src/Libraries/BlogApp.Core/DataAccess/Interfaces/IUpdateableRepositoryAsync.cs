using BlogApp.Core.Entities.Base;

namespace BlogApp.Core.DataAccess.Interfaces;
public interface IUpdateableRepositoryAsync<TEntity> : IRepositoryAsync where TEntity : BaseEntity
{
    Task<TEntity> UpdateAsync(TEntity entity);
}
