using BlogApp.Core.Entities.Base;

namespace BlogApp.Core.DataAccess.Interfaces.Repositories;
public interface IDeleteableRepository<TEntity> where TEntity : BaseEntity
{
    void Delete(TEntity entity);
}
