using BlogApp.Core.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.DbSets;

namespace BlogApp.DataAccess.Interfaces.Repositories;
public interface IArticleRepository : IAsyncFindableRepository<Article>, IAsyncQueryableRepository<Article>, IAsyncInsertableRepository<Article>, IAsyncRepository
{
}
