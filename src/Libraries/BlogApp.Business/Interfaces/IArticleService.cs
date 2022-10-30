using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.Entities.Dtos.Articles;
using BlogApp.Entities.Dtos.PublishedArticles;

namespace BlogApp.Business.Interfaces;
public interface IArticleService
{
    Task<IDataResult<List<PublishedArticleByUserListDto>>> GetAllPublishedAsync();
    Task<IDataResult<List<PublishedArticleByUserListDto>>> GetAllPublishedByUserIdAsync(Guid userId);
    Task<IDataResult<List<ArticleDto>>> GetTrendsAsync();
    Task<IDataResult<PublishedArticleDetailsDto>> GetByIdAsync(Guid id);
    Task<IResult> PublishAsync(Guid articleId);
    Task<IResult> AddAsync(ArticleCreateDto createArticleDto);
    Task<IDataResult<List<ArticleUnpublishedListDto>>> GetAllUnpublishedByUserIdAsync(Guid userId);
    Task<IDataResult<ArticleUnpublishedDetailsDto>> GetUnpublishedByIdAsync(Guid id);
    Task<IDataResult<List<PublishedArticleListDto>>> GetAllPublishedByTopicNameAsync(string topicName);
    Task<IDataResult<List<PublishedArticleShortDetailsDto>>> GetRandomArticlesWithShortDetails();
}
