using BlogApp.MVCUI.Models.Articles;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVCUI.Views.Shared.Components.ArticleComments;

public class ArticleCommentsViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(Guid articleId)
    {
        return View(Enumerable.Empty<ArticleCommentListVM>());
    }
}
