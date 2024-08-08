using BlogApp.Business.Interfaces;
using BlogApp.Entities.Dtos.Articles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.RateLimiting;

namespace BlogApp.API.Controllers.v1;

[EnableRateLimiting("Basic")]
public class ArticlesController(IArticleService articleService)
    : BaseController
{
    [HttpGet]
    [OutputCache]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var result = await articleService.GetAllPublishedAsync(cancellationToken);

        return GetDataResult(result);
    }

    [HttpGet("Published")]
    public async Task<IActionResult> GetAllPublished(CancellationToken cancellationToken = default)
    {
        var result = await articleService.GetAllPublishedByUserIdAsync(UserId, cancellationToken);

        return GetDataResult(result);
    }

    [HttpGet("{topicName}")]
    public async Task<IActionResult> GetAllPublished(string topicName, CancellationToken cancellationToken = default)
    {
        var result = await articleService.GetAllPublishedByTopicNameAsync(topicName, cancellationToken);

        return GetDataResult(result);
    }

    [HttpGet("Unpublished")]
    public async Task<IActionResult> GetAllUnpublished(CancellationToken cancellationToken = default)
    {
        var result = await articleService.GetAllUnpublishedByUserIdAsync(UserId, cancellationToken);

        return GetDataResult(result);
    }

    [HttpGet("Unpublished/{id:guid}")]
    public async Task<IActionResult> GetUnpublishedById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var result = await articleService.GetUnpublishedByIdAsync(id, cancellationToken);

        return GetDataResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var result = await articleService.GetByIdAsync(id, cancellationToken);

        return GetDataResult(result);
    }

    [HttpGet("ShortDetails")]
    public async Task<IActionResult> GetShortDetails(CancellationToken cancellationToken = default)
    {
        var result = await articleService.GetRandomArticlesWithShortDetails(cancellationToken);

        return GetDataResult(result);
    }

    [HttpGet("Trends")]
    [OutputCache]
    public async Task<IActionResult> GetTrends(CancellationToken cancellationToken = default)
    {
        var result = await articleService.GetTrendsAsync(cancellationToken);

        return GetDataResult(result);
    }

    [HttpPost]
    [DisableRateLimiting]
    public async Task<IActionResult> Create([FromBody] ArticleCreateDto createArticleDto, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        createArticleDto.UserId = UserId;
        var result = await articleService.AddAsync(createArticleDto, cancellationToken);

        return Ok(result);
    }

    [HttpPost("Publish")]
    public async Task<IActionResult> Publish([FromBody] Guid id, CancellationToken cancellationToken = default)
    {
        var result = await articleService.PublishAsync(id, cancellationToken);

        return GetResult(result);
    }
}