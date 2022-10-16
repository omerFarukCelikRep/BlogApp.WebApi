using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVCUI.Views.Shared.Components.ArticleAuthorInfo;

public class ArticleAuthorInfoViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(Guid userId)
    {
        return View();
    }
}
