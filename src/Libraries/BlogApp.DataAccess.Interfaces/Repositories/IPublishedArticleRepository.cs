using BlogApp.Core.DataAccess.Interfaces;
using BlogApp.Entities.Concrete;

namespace BlogApp.DataAccess.Interfaces.Repositories;
public interface IPublishedArticleRepository : IInsertableRepositoryAsync<PublishedArticle>, IOrderableRepositoryAsync<PublishedArticle>, IQueryableRepositoryAsync<PublishedArticle>, IRepositoryAsync
{
}
