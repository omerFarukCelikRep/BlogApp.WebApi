using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.Entities.Dtos.Comments;

namespace BlogApp.Business.Interfaces;
public interface ICommentService
{
    Task<IDataResult<CommentCreatedDto>> AddAsync(CommentCreateDto commentCreateDto);
    Task<IDataResult<List<ArticleCommentListDto>>> GetAllByArticleIdAsync(Guid articleId);
}
