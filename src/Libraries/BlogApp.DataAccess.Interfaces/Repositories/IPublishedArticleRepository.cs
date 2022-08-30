using BlogApp.Core.DataAccess.Interfaces;
using BlogApp.Entities.Concrete;

namespace BlogApp.DataAccess.Interfaces.Repositories;
public interface IPublishedArticleRepository : IAsyncInsertableRepository<PublishedArticle>, IAsyncOrderableRepository<PublishedArticle>, IAsyncQueryableRepository<PublishedArticle>, IAsyncRepository
{
}
