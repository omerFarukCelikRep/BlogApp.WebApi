using BlogApp.Core.DataAccess.Abstract;
using BlogApp.Entities.Concrete;

namespace BlogApp.DataAccess.Abstract;
public interface IPublishedArticleRepository : IRepositoryAsync<PublishedArticle>
{
}
