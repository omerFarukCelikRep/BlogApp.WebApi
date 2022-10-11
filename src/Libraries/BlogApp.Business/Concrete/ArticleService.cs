using BlogApp.Business.Constants;
using BlogApp.Business.Helpers;
using BlogApp.Business.Interfaces;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.Concrete;
using BlogApp.Entities.Dtos.Articles;

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

    public async Task<IDataResult<List<ArticlePublishedListDto>>> GetAllPublishedAsync()
    {
        var articles = await _publishedArticleRepository.GetAllAsync(false);
        var mappedArticles = ObjectMapper.Mapper.Map<List<ArticlePublishedListDto>>(articles);

        return new SuccessDataResult<List<ArticlePublishedListDto>>(mappedArticles, ServiceMessages.ArticlesListed);
    }

    public async Task<IDataResult<List<ArticleUnpublishedListDto>>> GetAllUnpublishedByUserIdAsync(Guid userId)
    {
        var publishedArticleIds = (await _publishedArticleRepository.GetAllAsync(x => x.Article.MemberId == userId, false)).Select(x => x.Id).ToList();
        var articles = await _articleRepository.GetAllAsync(x => x.MemberId == userId, true);
        var unpublishedArticles = articles.Where(article => !publishedArticleIds.Any(publishedArticleId => publishedArticleId == article.Id)).ToList();

        var mappedArticles = ObjectMapper.Mapper.Map<List<ArticleUnpublishedListDto>>(unpublishedArticles);
        return new SuccessDataResult<List<ArticleUnpublishedListDto>>(mappedArticles, ServiceMessages.ArticlesListed);
    }

    public async Task<IDataResult<ArticleUnpublishedDetailsDto>> GetUnpublishedById(Guid id)
    {
        var article = await _articleRepository.GetByIdAsync(id, false);
        if (article is null)
        {
            return new ErrorDataResult<ArticleUnpublishedDetailsDto>(ServiceMessages.ArticleNotFound);
        }

        var mappedArticle = ObjectMapper.Mapper.Map<ArticleUnpublishedDetailsDto>(article);
        return new SuccessDataResult<ArticleUnpublishedDetailsDto>(mappedArticle, ServiceMessages.ArticlesListed);
    }

    public async Task<IDataResult<ArticleDto>> GetByIdAsync(Guid id)
    {
        var article = await _articleRepository.GetByIdAsync(id, false);
        if (article is null)
        {
            return new ErrorDataResult<ArticleDto>(ServiceMessages.ArticleNotFound);
        }

        var mappedArticle = ObjectMapper.Mapper.Map<ArticleDto>(article);

        return new SuccessDataResult<ArticleDto>(mappedArticle, ServiceMessages.ArticlesListed);
    }

    public async Task<IDataResult<List<ArticleDto>>> GetTrendsAsync()
    {
        var articles = await _publishedArticleRepository.GetAllAsync(orderby: x => x.ReadingCount, orderDesc: true, tracking: false);

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
        if (await _publishedArticleRepository.SaveChangesAsync() <= 0)
        {
            return new ErrorResult("İşlem Başarısız");  //TODO:Magic string
        }

        return new SuccessResult(ServiceMessages.ArticlePublished);
    }

    public async Task<IResult> AddAsync(ArticleCreateDto articleCreateDto)
    {
        var article = ObjectMapper.Mapper.Map<Article>(articleCreateDto);

        article.ReadTime = ArticleHelper.CalculateReadTime(articleCreateDto.Content);
        articleCreateDto.Topics.ForEach(topicId => article.ArticleTopics.Add(new() { ArticleId = article.Id, TopicId = topicId }));

        article = await _articleRepository.AddAsync(article);
        if (await _articleRepository.SaveChangesAsync() <= 0)
        {
            return new ErrorResult("İşlem Başarısız");  //TODO:Magic string
        }

        return new SuccessResult("Ekleme Gerçekleşti"); //TODO: Magic string
    }
}
