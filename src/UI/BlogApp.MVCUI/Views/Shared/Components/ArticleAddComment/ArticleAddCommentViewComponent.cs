using BlogApp.MVCUI.Models.Comments;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVCUI.Views.Shared.Components.ArticleAddComment;

public class ArticleAddCommentViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(Guid articleId)
    {
        return await Task.FromResult(View(new CommentAddVM()
        {
            ArticleId = articleId
        }));
    }
}
