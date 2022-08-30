using BlogApp.Business.Interfaces;
using BlogApp.Core.Utilities.Authentication;
using BlogApp.Core.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class BaseController : ControllerBase
{
    protected string UserIdentityId => JwtHelper.GetUserIdByToken(HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last());

    protected async Task<Guid> GetUserId([FromServices] IUserService userService)
    {
        return await userService.GetUserIdByIdentityId(Guid.Parse(UserIdentityId));
    }

    protected IActionResult GetResult(Core.Utilities.Results.Abstract.IResult result)
    {
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    protected IActionResult GetDataResult<T>(IDataResult<T> result)
    {
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
