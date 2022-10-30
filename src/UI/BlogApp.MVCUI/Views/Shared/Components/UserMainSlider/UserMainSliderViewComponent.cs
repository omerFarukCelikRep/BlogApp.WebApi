using BlogApp.MVCUI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVCUI.Views.Shared.Components.UserMainSlider;

public class UserMainSliderViewComponent : ViewComponent
{
    private readonly IArticleService _articleService;
    public UserMainSliderViewComponent(IArticleService articleService)
    {
        _articleService = articleService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = await _articleService.GetAllPublishedShortDetailsRandomly();
        return View(result?.Data);
    }
}
