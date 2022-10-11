using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.Entities.Dtos.Articles;

namespace BlogApp.Business.Interfaces;
public interface IArticleService
{
    Task<IDataResult<List<ArticlePublishedListDto>>> GetAllPublishedAsync();
    Task<IDataResult<List<ArticleDto>>> GetTrendsAsync();
    Task<IDataResult<ArticleDto>> GetByIdAsync(Guid id);
    Task<IResult> PublishAsync(Guid articleId);
    Task<IResult> AddAsync(ArticleCreateDto createArticleDto);
    Task<IDataResult<List<ArticleUnpublishedListDto>>> GetAllUnpublishedByUserIdAsync(Guid userId);
    Task<IDataResult<ArticleUnpublishedDetailsDto>> GetUnpublishedById(Guid id);
}
