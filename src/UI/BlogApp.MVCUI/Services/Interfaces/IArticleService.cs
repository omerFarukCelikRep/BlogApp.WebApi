using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.MVCUI.Models.Articles;

namespace BlogApp.MVCUI.Services.Interfaces;

public interface IArticleService
{
    Task<Core.Utilities.Results.Interfaces.IResult> AddAsync(ArticleAddVM articleAddVM);
    Task<IDataResult<List<ArticleUnpublishedListVM>>> GetAllUnpublished();
    Task<IDataResult<ArticleUnpublishedDetailsVM>> GetUnpublishedById(Guid articleId);
}
