using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.Entities.Dtos.Articles;
using BlogApp.Entities.Dtos.PublishedArticles;

namespace BlogApp.Business.Interfaces;
public interface IArticleService
{
    Task<IResult<List<PublishedArticleByUserListDto>?>> GetAllPublishedAsync(CancellationToken cancellationToken = default);
    Task<IResult<List<PublishedArticleByUserListDto>?>> GetAllPublishedByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IResult<List<ArticleDto>?>> GetTrendsAsync(CancellationToken cancellationToken = default);
    Task<IResult<PublishedArticleDetailsDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IResult> PublishAsync(Guid articleId, CancellationToken cancellationToken = default);
    Task<IResult> AddAsync(ArticleCreateDto createArticleDto, CancellationToken cancellationToken = default);
    Task<IResult<List<ArticleUnpublishedListDto>?>> GetAllUnpublishedByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IResult<ArticleUnpublishedDetailsDto?>> GetUnpublishedByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IResult<List<PublishedArticleListDto>?>> GetAllPublishedByTopicNameAsync(string topicName, CancellationToken cancellationToken = default);
    Task<IResult<List<PublishedArticleShortDetailsDto>?>> GetRandomArticlesWithShortDetails(CancellationToken cancellationToken = default);
}