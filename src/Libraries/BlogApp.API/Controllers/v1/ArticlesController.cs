using BlogApp.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers.v1;

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
        var result = await _articleService.GetAllAsync();

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _articleService.GetByIdAsync(id);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet("Trends")]
    public async Task<IActionResult> GetTrends()
    {
        var result = await _articleService.GetTrendsAsync();

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
