﻿using BlogApp.Business.Interfaces;
using BlogApp.Entities.Dtos.Articles;
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

        return GetDataResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _articleService.GetByIdAsync(id);

        return GetDataResult(result);
    }

    [HttpGet("Trends")]
    public async Task<IActionResult> GetTrends()
    {
        var result = await _articleService.GetTrendsAsync();

        return GetDataResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ArticleCreateDto createArticleDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _articleService.AddAsync(createArticleDto);

        return Ok();
    }

    [HttpPost("Publish")]
    public async Task<IActionResult> Publish([FromQuery] Guid id)
    {
        var result = await _articleService.PublishAsync(id);

        return GetResult(result);
    }
}
