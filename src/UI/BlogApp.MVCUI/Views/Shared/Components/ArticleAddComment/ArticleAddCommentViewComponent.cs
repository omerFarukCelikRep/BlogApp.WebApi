using BlogApp.MVCUI.Models.Comments;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVCUI.Views.Shared.Components.ArticleAddComment;

public class ArticleAddCommentViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(new CommentAddVM());
    }
}
