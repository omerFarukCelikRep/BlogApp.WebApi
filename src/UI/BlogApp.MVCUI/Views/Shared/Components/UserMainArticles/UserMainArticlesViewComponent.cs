using BlogApp.MVCUI.Models.Articles;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVCUI.Views.Shared.Components.UserMainArticles;

public class UserMainArticlesViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        await Task.CompletedTask;
        return View(Enumerable.Empty<UserMainArticleListVM>());
    }
}
