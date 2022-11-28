using BlogApp.Core.Entities.Base;

namespace BlogApp.Core.DataAccess.Interfaces.Repositories;
public interface IAsyncUpdateableRepository<TEntity> : IAsyncRepository where TEntity : BaseEntity
{
    Task<TEntity> UpdateAsync(TEntity entity);
}
