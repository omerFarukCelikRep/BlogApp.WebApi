using BlogApp.MVCUI.Models.Articles;
using BlogApp.MVCUI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApp.MVCUI.Controllers;
public class ArticleController : Controller
{
    private readonly ITopicService _topicService;
    public ArticleController(ITopicService topicService)
    {
        _topicService = topicService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var result = await _topicService.GetAll();
        return View(new ArticleAddVM
        {
            Topics = result.Data.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList()
        });
    }
}
