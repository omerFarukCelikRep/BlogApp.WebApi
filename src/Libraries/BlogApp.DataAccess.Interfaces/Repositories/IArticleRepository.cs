using BlogApp.Core.DataAccess.Interfaces;
using BlogApp.Entities.Concrete;

namespace BlogApp.DataAccess.Interfaces.Repositories;
public interface IArticleRepository : IAsyncFindableRepository<Article>, IAsyncQueryableRepository<Article>, IAsyncInsertableRepository<Article>, IAsyncRepository
{
}
