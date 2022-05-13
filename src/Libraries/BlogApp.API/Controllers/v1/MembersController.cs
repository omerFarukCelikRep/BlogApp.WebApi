using BlogApp.Business.Abstract;
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
}
