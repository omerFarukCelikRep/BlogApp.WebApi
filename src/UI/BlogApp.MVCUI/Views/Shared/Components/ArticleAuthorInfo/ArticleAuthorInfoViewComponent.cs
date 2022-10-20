using BlogApp.MVCUI.Models.Articles;
using BlogApp.MVCUI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVCUI.Views.Shared.Components.ArticleAuthorInfo;

public class ArticleAuthorInfoViewComponent : ViewComponent
{
    private readonly IUserService _userService;

    public ArticleAuthorInfoViewComponent(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IViewComponentResult> InvokeAsync(Guid userId)
    {
        var result = await _userService.GetArticleUserInfo(userId);
        return View(result.IsSuccess ? result.Data : new ArticleAuthorInfoVM());
    }
}
