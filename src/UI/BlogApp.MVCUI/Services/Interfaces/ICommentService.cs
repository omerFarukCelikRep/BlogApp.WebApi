using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.MVCUI.Models.Articles;
using BlogApp.MVCUI.Models.Comments;

namespace BlogApp.MVCUI.Services.Interfaces;

public interface ICommentService
{
    Task<Core.Utilities.Results.Interfaces.IResult> AddAsync(CommentAddVM commentAddVM);
    Task<IDataResult<List<ArticleCommentListVM>>> GetAllByArticleId(Guid articleId);
}
