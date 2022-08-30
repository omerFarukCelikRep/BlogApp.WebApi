using BlogApp.Core.Entities.Base;

namespace BlogApp.Core.DataAccess.Interfaces;
public interface IAsyncDeleteableRepository<TEntity> : IAsyncRepository where TEntity : BaseEntity
{
    Task DeleteAsync(TEntity entity);
}
