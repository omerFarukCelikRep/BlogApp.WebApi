using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.Entities.Dtos.Comments;

namespace BlogApp.Business.Interfaces;
public interface ICommentService
{
    Task<IResult<CommentCreatedDto?>> AddAsync(CommentCreateDto commentCreateDto, CancellationToken cancellationToken = default);
    Task<IResult<List<ArticleCommentListDto>?>> GetAllByArticleIdAsync(Guid articleId, CancellationToken cancellationToken = default);
}
