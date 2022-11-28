using BlogApp.Core.DataAccess.Interfaces.Models;
using BlogApp.Core.Entities.Base;
using System.Linq.Expressions;

namespace BlogApp.Core.DataAccess.Interfaces.Repositories;
public interface IAsyncPaginateRepository<TEntity> : IAsyncRepository where TEntity : BaseEntity
{
    Task<IPaginate<TEntity>> GetAllAsPaginateAsync(int index = 0, int size = 10, bool tracking = true);
    Task<IPaginate<TEntity>> GetAllAsPaginateAsync(Expression<Func<TEntity, bool>> expression, int index = 0, int size = 10, bool tracking = true);
}
