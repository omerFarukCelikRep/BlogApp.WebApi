using BlogApp.Business.Abstract;
using BlogApp.Entities.Dtos.Members;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers.v1;

public class MembersController : BaseController
{
    private readonly IMemberService _memberService;

    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _memberService.GetAllAsync();

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _memberService.GetById(id, false);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateMemberDto updateMember)
    {
        var result = await _memberService.UpdateAsync(updateMember);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
