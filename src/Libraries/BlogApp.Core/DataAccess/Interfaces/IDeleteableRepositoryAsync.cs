using BlogApp.Core.Entities.Base;

namespace BlogApp.Core.DataAccess.Interfaces;
public interface IDeleteableRepositoryAsync<TEntity> : IRepositoryAsync where TEntity : BaseEntity
{
    Task<bool> DeleteAsync(TEntity entity);
}
