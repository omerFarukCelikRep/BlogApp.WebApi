using BlogApp.Core.Utilities.Results.Abstract;
using BlogApp.MVCUI.Models.Articles;

namespace BlogApp.MVCUI.Services.Interfaces;

public interface IArticleService
{
    Task<Core.Utilities.Results.Abstract.IResult> AddAsync(ArticleAddVM articleAddVM);
    Task<IDataResult<List<ArticleUnpublishedListVM>>> GetAllUnpublished();
    Task<IDataResult<ArticleUnpublishedDetailsVM>> GetUnpublishedById(Guid articleId);
}
