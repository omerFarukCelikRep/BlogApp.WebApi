using BlogApp.Business.Interfaces;
using BlogApp.Entities.Dtos.Users;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers.v1;

public class UsersController : BaseController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _userService.GetAllAsync();

        return GetDataResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _userService.GetByIdAsync(id);

        return GetDataResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UserUpdateDto updateMember)
    {
        var result = await _userService.UpdateAsync(updateMember);

        return GetDataResult(result);
    }
}
