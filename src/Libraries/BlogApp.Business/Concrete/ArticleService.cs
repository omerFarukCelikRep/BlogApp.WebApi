using BlogApp.Business.Constants;
using BlogApp.Business.Interfaces;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Results.Abstract;
using BlogApp.Core.Utilities.Results.Concrete;
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

    public async Task<IDataResult<List<ArticleDto>>> GetAllAsync()
    {
        var articles = await _publishedArticleRepository.GetAllAsync(false);

        if (!articles.Any())
        {
            return new ErrorDataResult<List<ArticleDto>>(ServiceMessages.ArticleNotFound);
        }

        var mappedArticles = ObjectMapper.Mapper.Map<List<ArticleDto>>(articles);

        return new SuccessDataResult<List<ArticleDto>>(mappedArticles, ServiceMessages.ArticlesListed);
    }

    public async Task<IDataResult<ArticleDto>> GetByIdAsync(Guid id)
    {
        var article = await _articleRepository.GetByIdAsync(id);

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

        _ = await _publishedArticleRepository.AddAsync(publishArticle);

        return new SuccessResult(ServiceMessages.ArticlePublished);
    }
}
