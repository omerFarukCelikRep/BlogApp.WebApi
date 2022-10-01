using BlogApp.MVCUI.Filters;
using BlogApp.MVCUI.Models.Topics;
using BlogApp.MVCUI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVCUI.Controllers;
[AuthorizationFilter]
public class TopicController : BaseController
{
    private readonly ITopicService _topicService;
    public TopicController(ITopicService topicService)
    {
        _topicService = topicService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var result = await _topicService.GetAll();
        return View(result.Data);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(TopicAddVM topicAddVM)
    {
        if (!ModelState.IsValid)
        {
            return View(topicAddVM);
        }

        var result = await _topicService.AddAsync(topicAddVM);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View(topicAddVM);
        }

        return RedirectToAction(nameof(Index));
    }
}
