using BlogApp.Business.Abstract;
using BlogApp.Entities.Dtos.Topics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("{id}", Name = "GetByIdAsync")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _topicService.GetByIdAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTopicDto createTopicDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(createTopicDto);
        }

        var result = await _topicService.AddAsync(createTopicDto);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return CreatedAtRoute(nameof(GetByIdAsync), new { Id = result.Data.Id}, result);
    }

    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateTopicDto updateTopicDto)
    //{
    //    var topic = await _topicService.GetByIdAsync(id);

    //    if (topic is { Length > 0})
    //    {

    //    }
    //}
}
