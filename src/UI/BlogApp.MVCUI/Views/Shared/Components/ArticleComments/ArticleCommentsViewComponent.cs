using BlogApp.MVCUI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVCUI.Views.Shared.Components.ArticleComments;

public class ArticleCommentsViewComponent : ViewComponent
{
    private readonly ICommentService _commentService;
    public ArticleCommentsViewComponent(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public async Task<IViewComponentResult> InvokeAsync(Guid articleId)
    {
        var result = await _commentService.GetAllByArticleId(articleId);
        return View(result.Data);
    }
}
