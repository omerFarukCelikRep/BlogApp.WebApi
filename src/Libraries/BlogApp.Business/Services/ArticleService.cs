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

namespace BlogApp.Business.Concrete;
public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IPublishedArticleRepository _publishedArticleRepository;

    public ArticleService(IArticleRepository articleRepository, IPublishedArticleRepository publishedArticleRepository)
    {
        _articleRepository = articleRepository;
        _publishedArticleRepository = publishedArticleRepository;
    }

    public async Task<IDataResult<List<PublishedArticleByUserListDto>>> GetAllPublishedAsync()
    {
        var articles = await _publishedArticleRepository.GetAllAsync();
        var mappedArticles = ObjectMapper.Mapper.Map<List<PublishedArticleByUserListDto>>(articles);

        return new SuccessDataResult<List<PublishedArticleByUserListDto>>(mappedArticles, ServiceMessages.ArticlesListed);
    }

    public async Task<IDataResult<List<PublishedArticleByUserListDto>>> GetAllPublishedByUserIdAsync(Guid userId)
    {
        var articles = await _publishedArticleRepository.GetAllAsync(expression: x => x.Article!.UserId == userId, tracking: true);
        var mappedArticles = ObjectMapper.Mapper.Map<List<PublishedArticleByUserListDto>>(articles);

        return new SuccessDataResult<List<PublishedArticleByUserListDto>>(mappedArticles, ServiceMessages.ArticlesListed);
    }

    public async Task<IDataResult<List<ArticleUnpublishedListDto>>> GetAllUnpublishedByUserIdAsync(Guid userId)
    {
        var publishedArticleIds = (await _publishedArticleRepository.GetAllAsync(expression: x => x.Article!.UserId == userId, false)).Select(x => x.Id).ToList();
        var articles = await _articleRepository.GetAllAsync(x => x.UserId == userId);
        var unpublishedArticles = articles.Where(article => !publishedArticleIds.Any(publishedArticleId => publishedArticleId == article.Id)).ToList();

        var mappedArticles = ObjectMapper.Mapper.Map<List<ArticleUnpublishedListDto>>(unpublishedArticles);
        return new SuccessDataResult<List<ArticleUnpublishedListDto>>(mappedArticles, ServiceMessages.ArticlesListed);
    }

    public async Task<IDataResult<ArticleUnpublishedDetailsDto>> GetUnpublishedByIdAsync(Guid id)
    {
        var article = await _articleRepository.GetByIdAsync(id);
        if (article is null)
        {
            return new ErrorDataResult<ArticleUnpublishedDetailsDto>(ServiceMessages.ArticleNotFound);
        }

        var mappedArticle = ObjectMapper.Mapper.Map<ArticleUnpublishedDetailsDto>(article);
        return new SuccessDataResult<ArticleUnpublishedDetailsDto>(mappedArticle, ServiceMessages.ArticlesListed);
    }

    public async Task<IDataResult<PublishedArticleDetailsDto>> GetByIdAsync(Guid id)
    {
        var article = await _publishedArticleRepository.GetByIdAsync(id);
        if (article is null)
        {
            return new ErrorDataResult<PublishedArticleDetailsDto>(ServiceMessages.ArticleNotFound);
        }

        var mappedArticle = ObjectMapper.Mapper.Map<PublishedArticleDetailsDto>(article);
        return new SuccessDataResult<PublishedArticleDetailsDto>(mappedArticle, ServiceMessages.ArticlesListed);
    }

    public async Task<IDataResult<List<ArticleDto>>> GetTrendsAsync(CancellationToken cancellationToken = default)
    {
        var articles = await _publishedArticleRepository.GetAllAsync(orderby: x => x.ReadingCount, orderDesc: true, tracking: false, cancellationToken);

        articles = articles.Take(10).ToList();

        if (!articles.Any())
        {
            return new ErrorDataResult<List<ArticleDto>>(ServiceMessages.ArticleNotFound);
        }

        var mappedArticles = ObjectMapper.Mapper.Map<List<ArticleDto>>(articles);

        return new SuccessDataResult<List<ArticleDto>>(mappedArticles, ServiceMessages.ArticlesListed);
    }

    public async Task<IResult> PublishAsync(Guid articleId)
    {
        var article = await _articleRepository.GetByIdAsync(articleId);

        if (article is null)
        {
            return new ErrorResult(ServiceMessages.ArticleNotFound);
        }

        PublishedArticle publishArticle = new()
        {
            Id = article.Id,
            Article = article,
            PublishDate = DateTime.Now
        };

        await _publishedArticleRepository.AddAsync(publishArticle);
        await _publishedArticleRepository.SaveChangesAsync();

        return new SuccessResult(ServiceMessages.ArticlePublished);
    }

    public async Task<IResult> AddAsync(ArticleCreateDto articleCreateDto)
    {
        var article = ObjectMapper.Mapper.Map<Article>(articleCreateDto);

        article.ReadTime = ArticleHelper.CalculateReadTime(articleCreateDto.Content);
        articleCreateDto.Topics.ForEach(topicId => article.ArticleTopics.Add(new() { ArticleId = article.Id, TopicId = topicId }));

        article = await _articleRepository.AddAsync(article);
        await _articleRepository.SaveChangesAsync();

        return new SuccessResult("Ekleme Gerçekleşti"); //TODO: Magic string
    }

    public async Task<IDataResult<List<PublishedArticleListDto>>> GetAllPublishedByTopicNameAsync(string topicName)
    {
        var articles = await _publishedArticleRepository.GetAllAsync(expression: x => x.Article!.ArticleTopics.Any(at => at.Topic!.Name.ToLower().Contains(topicName.Trim().ToLower())), true);
        var mappedArticles = ObjectMapper.Mapper.Map<List<PublishedArticleListDto>>(articles);

        return new SuccessDataResult<List<PublishedArticleListDto>>(mappedArticles, ServiceMessages.ArticlesListed);
    }

    public async Task<IDataResult<List<PublishedArticleShortDetailsDto>>> GetRandomArticlesWithShortDetails()
    {
        var articles = await _publishedArticleRepository.GetAllAsync(x => Guid.NewGuid(), takeCount: 3);

        var mappedArticle = ObjectMapper.Mapper.Map<List<PublishedArticleShortDetailsDto>>(articles);
        return new SuccessDataResult<List<PublishedArticleShortDetailsDto>>(mappedArticle, ServiceMessages.ArticlesListed);
    }
}
