using BlogApp.MVCUI.Models.Articles;
using BlogApp.MVCUI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApp.MVCUI.Controllers;
public class ArticleController : BaseController
{
    private readonly ITopicService _topicService;
    private readonly IArticleService _articleService;
    public ArticleController(ITopicService topicService, IArticleService articleService)
    {
        _topicService = topicService;
        _articleService = articleService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View(new ArticleAddVM
        {
            Topics = await GetTopics()
        });
    }

    [HttpPost]
    public async Task<IActionResult> Add(ArticleAddVM articleAddVM)
    {
        if (!ModelState.IsValid)
        {
            articleAddVM.Topics = await GetTopics();
            return View(articleAddVM);
        }

        var result = await _articleService.AddAsync(articleAddVM);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View(articleAddVM);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Unpublished()
    {
        var result = await _articleService.GetAllUnpublished();
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return RedirectToAction(nameof(Index));
        }

        return View(result.Data);
    }

    public async Task<List<SelectListItem>> GetTopics(Guid? selectedId = null)
    {
        var topics = (await _topicService.GetAll()).Data;
        return topics.ConvertAll(topic => new SelectListItem
        {
            Selected = selectedId is not null && selectedId == topic.Id,
            Text = topic.Name,
            Value = topic.Id.ToString()
        });
    }
}
