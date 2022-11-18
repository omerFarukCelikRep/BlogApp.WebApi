using BlogApp.Business.Interfaces;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.Entities.Dtos.Articles;
using BlogApp.Entities.Dtos.PublishedArticles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BlogApp.API.Controllers.v1;

[EnableRateLimiting("Basic")]
public class ArticlesController : BaseController
{
    private readonly IArticleService _articleService;

    public ArticlesController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _articleService.GetAllPublishedAsync();

        return GetDataResult(result);
    }

    [HttpGet("Published")]
    public async Task<IActionResult> GetAllPublished()
    {
        var result = await _articleService.GetAllPublishedByUserIdAsync(UserId);

        return GetDataResult(result);
    }

    [HttpGet("{topicName}")]
    public async Task<IActionResult> GetAllPublished(string topicName)
    {
        IDataResult<List<PublishedArticleListDto>> result = await _articleService.GetAllPublishedByTopicNameAsync(topicName);

        return GetDataResult(result);
    }

    [HttpGet("Unpublished")]
    public async Task<IActionResult> GetAllUnpublished()
    {
        var result = await _articleService.GetAllUnpublishedByUserIdAsync(UserId);

        return GetDataResult(result);
    }

    [HttpGet("Unpublished/{id:guid}")]
    public async Task<IActionResult> GetUnpublishedById([FromRoute] Guid id)
    {
        var result = await _articleService.GetUnpublishedByIdAsync(id);

        return GetDataResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _articleService.GetByIdAsync(id);

        return GetDataResult(result);
    }

    [HttpGet("ShortDetails")]
    public async Task<IActionResult> GetShortDetails()
    {
        var result = await _articleService.GetRandomArticlesWithShortDetails();

        return GetDataResult(result);
    }

    [HttpGet("Trends")]
    public async Task<IActionResult> GetTrends()
    {
        var result = await _articleService.GetTrendsAsync();

        return GetDataResult(result);
    }

    [HttpPost]
    [DisableRateLimiting]
    public async Task<IActionResult> Create([FromBody] ArticleCreateDto createArticleDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        createArticleDto.UserId = UserId;
        var result = await _articleService.AddAsync(createArticleDto);

        return Ok(result);
    }

    [HttpPost("Publish")]
    public async Task<IActionResult> Publish([FromBody] Guid id)
    {
        var result = await _articleService.PublishAsync(id);

        return GetResult(result);
    }
}
