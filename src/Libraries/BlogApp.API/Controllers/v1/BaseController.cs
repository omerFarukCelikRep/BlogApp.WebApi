﻿using BlogApp.Business.Interfaces;
using BlogApp.Core.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApp.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BaseController : ControllerBase
{
    protected string UserIdentityId => User.FindFirstValue(ClaimTypes.NameIdentifier);

    protected async Task<Guid?> GetUserId([FromServices] IUserService userService)
    {
        return await userService.GetUserIdByIdentityIdAsync(Guid.Parse(UserIdentityId));
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
