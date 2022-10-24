using BlogApp.MVCUI.Models.Comments;
using BlogApp.MVCUI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVCUI.Controllers;
public class CommentController : Controller
{
    private readonly ICommentService _commentService;
    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(CommentAddVM commentAddVM)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Details", "Article", new { id = commentAddVM.ArticleId });
        }

        var result = await _commentService.AddAsync(commentAddVM);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message);
        }

        return RedirectToAction("Details", "Article", new { id = commentAddVM.ArticleId });
    }
}
