using BlogApp.MVCUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVCUI.Views.Shared.Components.FooterPopularTopics;

[ViewComponent(Name = "FooterPopularTopics")]
public class FooterPopularTopicsViewComponent : ViewComponent
{
    //private readonly ITopicService _topicService;
    public async Task<IViewComponentResult> InvokeAsync()
    {
        await Task.CompletedTask;

        return View(Enumerable.Empty<FooterTopicListVM>());
    }
}
