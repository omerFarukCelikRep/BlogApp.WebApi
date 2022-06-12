using BlogApp.Business.Interfaces;
using BlogApp.Entities.Dtos.Topics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogApp.API.Controllers.v1;
public class TopicsController : BaseController
{
    private readonly ITopicService _topicService;

    public TopicsController(ITopicService topicService)
    {
        _topicService = topicService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _topicService.GetAllAsync();

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _topicService.GetByIdAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTopicDto createTopicDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _topicService.AddAsync(createTopicDto);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return StatusCode((int)HttpStatusCode.Created, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateTopicDto updateTopicDto)
    {
        if (id != updateTopicDto.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _topicService.UpdateAsync(updateTopicDto);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _topicService.DeleteAsync(id);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
