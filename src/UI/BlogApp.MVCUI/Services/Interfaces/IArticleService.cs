using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.MVCUI.Models.Articles;

namespace BlogApp.MVCUI.Services.Interfaces;

public interface IArticleService
{
    Task<Core.Utilities.Results.Interfaces.IResult> AddAsync(ArticleAddVM articleAddVM);
    Task<IDataResult<List<ArticlePublishedListVM>>> GetAllPublished();
    Task<IDataResult<List<ArticlePublishedListVM>>> GetAllPublishedByTopicName(string topicName);
    Task<IDataResult<List<ArticleUnpublishedListVM>>> GetAllUnpublished();
    Task<IDataResult<ArticlePublishedDetailsVM>> GetPublishedById(Guid articleId);
    Task<IDataResult<ArticleUnpublishedDetailsVM>> GetUnpublishedById(Guid articleId);
    Task<Core.Utilities.Results.Interfaces.IResult> Publish(Guid articleId);
}
