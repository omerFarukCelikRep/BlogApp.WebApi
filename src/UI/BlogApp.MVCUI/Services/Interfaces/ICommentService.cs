using BlogApp.MVCUI.Models.Comments;

namespace BlogApp.MVCUI.Services.Interfaces;

public interface ICommentService
{
    Task<Core.Utilities.Results.Interfaces.IResult> AddAsync(CommentAddVM commentAddVM);
}
