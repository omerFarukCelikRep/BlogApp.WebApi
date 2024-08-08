using BlogApp.Business.Interfaces;
using BlogApp.Entities.Dtos.Users;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers.v1;

public class UsersController(IUserService userService)
    : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var result = await userService.GetAllAsync(cancellationToken);

        return GetDataResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var result = await userService.GetByIdAsync(id, cancellationToken);

        return GetDataResult(result);
    }

    [HttpGet("GetUserInfo")]
    public async Task<IActionResult> GetUserInfoById([FromQuery] Guid userId, CancellationToken cancellationToken = default)
    {
        var result = await userService.GetArticleUserInfoById(userId, cancellationToken);

        return GetDataResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UserUpdateDto updateMember, CancellationToken cancellationToken = default)
    {
        var result = await userService.UpdateAsync(updateMember, cancellationToken);

        return GetDataResult(result);
    }
}