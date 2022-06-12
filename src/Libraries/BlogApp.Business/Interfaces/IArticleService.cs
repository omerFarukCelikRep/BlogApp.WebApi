using BlogApp.Core.Utilities.Results.Abstract;
using BlogApp.Entities.Dtos.Articles;

namespace BlogApp.Business.Interfaces;
public interface IArticleService
{
    Task<IDataResult<List<ArticleDto>>> GetAllAsync();
    Task<IDataResult<List<ArticleDto>>> GetTrendsAsync();
    Task<IDataResult<ArticleDto>> GetByIdAsync(Guid id);
    Task<IResult> PublishAsync(Guid articleId);
}
