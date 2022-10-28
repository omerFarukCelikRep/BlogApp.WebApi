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
    public async Task<IActionResult> Index()
    {
        var result = await _articleService.GetAllPublished();

        return View(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> ListByTopic([FromQuery(Name = "t")] string topicName)
    {
        var result = await _articleService.GetAllPublishedByTopicName(topicName);

        return View(nameof(Index), result.Data);
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

    [HttpGet]
    public async Task<IActionResult> UnpublishedDetails(Guid id)
    {
        var result = await _articleService.GetUnpublishedById(id);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return RedirectToAction(nameof(Unpublished));
        }

        return View(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Publish(Guid id)
    {
        var result = await _articleService.Publish(id);

        return RedirectToAction(result.IsSuccess ? nameof(Index) : nameof(Unpublished));
    }

    [HttpGet]
    public async Task<IActionResult> PublishedDetails(Guid id)
    {
        var result = await _articleService.GetPublishedById(id);

        return View(result.Data);
    }

    private async Task<List<SelectListItem>> GetTopics(Guid? selectedId = null)
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
