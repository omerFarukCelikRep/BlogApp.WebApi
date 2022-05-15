using BlogApp.Core.Utilities.Results.Abstract;
using BlogApp.Entities.Dtos.Articles;

namespace BlogApp.Business.Abstract;
public interface IArticleService
{
    Task<IDataResult<List<ArticleDto>>> GetTrends();
}
