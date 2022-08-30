using BlogApp.Business.Interfaces;
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
        var result = await _memberService.GetAllAsync(tracking: false);

        return GetDataResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _memberService.GetByIdAsync(id: id, tracking: false);

        return GetDataResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateMemberDto updateMember)
    {
        var result = await _memberService.UpdateAsync(updateMember);

        return GetDataResult(result);
    }
}
