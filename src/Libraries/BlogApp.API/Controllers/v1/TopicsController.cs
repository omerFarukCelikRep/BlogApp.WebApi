using BlogApp.Business.Interfaces;
using BlogApp.Entities.Dtos.Topics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogApp.API.Controllers.v1;
public class TopicsController(ITopicService topicService)
    : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await topicService.GetAllAsync(cancellationToken);

        return GetDataResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await topicService.GetByIdAsync(id, cancellationToken);

        return GetDataResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] TopicCreateDto createTopicDto, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await topicService.AddAsync(createTopicDto, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return StatusCode((int)HttpStatusCode.Created, result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] TopicUpdateDto updateTopicDto, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await topicService.UpdateAsync(updateTopicDto, cancellationToken);

        return GetDataResult(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await topicService.DeleteAsync(id, cancellationToken);

        return GetResult(result);
    }
}