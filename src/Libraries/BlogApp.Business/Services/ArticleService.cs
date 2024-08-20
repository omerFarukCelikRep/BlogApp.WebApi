using BlogApp.Business.Constants;
using BlogApp.Business.Helpers;
using BlogApp.Business.Interfaces;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.DbSets;
using BlogApp.Entities.Dtos.Articles;
using BlogApp.Entities.Dtos.PublishedArticles;
using ArticleMessages = BlogApp.Business.Constants.ServiceMessages.Article;

namespace BlogApp.Business.Services;

public class ArticleService(IArticleRepository articleRepository,
                            IPublishedArticleRepository publishedArticleRepository)
    : IArticleService
{
    public async Task<IResult<List<PublishedArticleByUserListDto>?>> GetAllPublishedAsync(CancellationToken cancellationToken = default)
    {
        var articles = await publishedArticleRepository.GetAllAsync(cancellationToken: cancellationToken);
        var mappedArticles = ObjectMapper.Mapper.Map<List<PublishedArticleByUserListDto>>(articles);

        return Result<List<PublishedArticleByUserListDto>?>.Success(mappedArticles, ArticleMessages.Listed);
    }

    public async Task<IResult<List<PublishedArticleByUserListDto>?>> GetAllPublishedByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var articles = await publishedArticleRepository.GetAllAsync(expression: x => x.Article!.UserId == userId, tracking: true, cancellationToken: cancellationToken);
        var mappedArticles = ObjectMapper.Mapper.Map<List<PublishedArticleByUserListDto>>(articles);

        return Result<List<PublishedArticleByUserListDto>?>.Success(mappedArticles, ArticleMessages.Listed);
    }

    public async Task<IResult<List<ArticleUnpublishedListDto>?>> GetAllUnpublishedByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var publishedArticles = await publishedArticleRepository.GetAllAsync(expression: article => article.Article!.UserId == userId, tracking: false, cancellationToken: cancellationToken);
        List<Guid> publishedArticleIds = publishedArticles.Select(x => x.Id)
                                                          .ToList();
        IEnumerable<Article> articles = await articleRepository.GetAllAsync(x => x.UserId == userId, cancellationToken: cancellationToken);
        List<Article> unpublishedArticles = articles.Where(article => !publishedArticleIds.Any(publishedArticleId => publishedArticleId == article.Id))
                                                    .ToList();

        var mappedArticles = ObjectMapper.Mapper.Map<List<ArticleUnpublishedListDto>>(unpublishedArticles);

        return Result<List<ArticleUnpublishedListDto>>.Success(mappedArticles, ArticleMessages.Listed);
    }

    public async Task<IResult<ArticleUnpublishedDetailsDto?>> GetUnpublishedByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var article = await articleRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (article is null)
            return Result<ArticleUnpublishedDetailsDto?>.Failure(new Error("404", ArticleMessages.NotFound));

        var mappedArticle = ObjectMapper.Mapper.Map<ArticleUnpublishedDetailsDto>(article);
        return Result<ArticleUnpublishedDetailsDto>.Success(mappedArticle, ArticleMessages.Listed);
    }

    public async Task<IResult<PublishedArticleDetailsDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var article = await publishedArticleRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (article is null)
            return Result<PublishedArticleDetailsDto>.Failure(new Error("404", ArticleMessages.NotFound));

        var mappedArticle = ObjectMapper.Mapper.Map<PublishedArticleDetailsDto>(article);

        return Result<PublishedArticleDetailsDto>.Success(mappedArticle, ArticleMessages.Listed);
    }

    public async Task<IResult<List<ArticleDto>?>> GetTrendsAsync(CancellationToken cancellationToken = default)
    {
        var articles = await publishedArticleRepository.GetAllAsync(orderby: x => x.ReadingCount, orderDesc: true, tracking: false, cancellationToken);

        articles = articles.Take(10);

        if (!articles.Any())
            return Result<List<ArticleDto>>.Failure(new Error("404", ArticleMessages.NotFound));

        var mappedArticles = ObjectMapper.Mapper.Map<List<ArticleDto>>(articles);
        return Result<List<ArticleDto>>.Success(mappedArticles, ArticleMessages.Listed);
    }

    public async Task<IResult> PublishAsync(Guid articleId, CancellationToken cancellationToken = default)
    {
        var article = await articleRepository.GetByIdAsync(articleId, cancellationToken: cancellationToken);

        if (article is null)
            return Result.Failure(new Error("404", ArticleMessages.NotFound));

        PublishedArticle publishArticle = new()
        {
            Id = article.Id,
            Article = article,
            PublishDate = DateTime.Now
        };

        await publishedArticleRepository.AddAsync(publishArticle, cancellationToken);
        await publishedArticleRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(ArticleMessages.Published);
    }

    public async Task<IResult> AddAsync(ArticleCreateDto articleCreateDto, CancellationToken cancellationToken = default)
    {
        var article = ObjectMapper.Mapper.Map<Article>(articleCreateDto);

        article.ReadTime = ArticleHelper.CalculateReadTime(articleCreateDto.Content);
        foreach (var topic in articleCreateDto.Topics)
        {
            article.ArticleTopics.Add(new()
            {
                ArticleId = article.Id,
                TopicId = topic
            });
        }

        _ = await articleRepository.AddAsync(article, cancellationToken);
        await articleRepository.SaveChangesAsync(cancellationToken);

        return Result.Success("Ekleme Gerçekleşti"); //TODO: Magic string
    }

    public async Task<IResult<List<PublishedArticleListDto>?>> GetAllPublishedByTopicNameAsync(string topicName, CancellationToken cancellationToken = default)
    {
        var articles = await publishedArticleRepository.GetAllAsync(expression: x => x.Article!.ArticleTopics.Any(at => at.Topic!.Name.Contains(topicName.Trim(), StringComparison.CurrentCultureIgnoreCase)), true, cancellationToken: cancellationToken);
        var mappedArticles = ObjectMapper.Mapper.Map<List<PublishedArticleListDto>>(articles);

        return Result<List<PublishedArticleListDto>>.Success(mappedArticles, ArticleMessages.Listed);
    }

    public async Task<IResult<List<PublishedArticleShortDetailsDto>?>> GetRandomArticlesWithShortDetails(CancellationToken cancellationToken = default)
    {
        var articles = await publishedArticleRepository.GetAllAsync(x => Guid.NewGuid(), takeCount: 3, cancellationToken: cancellationToken);

        var mappedArticle = ObjectMapper.Mapper.Map<List<PublishedArticleShortDetailsDto>>(articles);
        return Result<List<PublishedArticleShortDetailsDto>>.Success(mappedArticle, ArticleMessages.Listed);
    }
}