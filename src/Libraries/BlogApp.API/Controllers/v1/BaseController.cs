using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace BlogApp.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BaseController
    : ControllerBase
{
    private const string DefaultIpAddress = "Local";
    protected string? UserIdentityId => User.FindFirstValue(ClaimTypes.NameIdentifier);
    protected Guid UserId
    {
        get
        {
            _ = Guid.TryParse(User.Claims.LastOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value, out var userId);
            return userId;
        }
    }

    protected IActionResult GetResult(Core.Utilities.Results.Interfaces.IResult result)
    {
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    protected IActionResult GetDataResult<T>(Core.Utilities.Results.Interfaces.IResult<T> result)
    {
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    protected string GetIpAddress()
    {
        if (Request.Headers.TryGetValue("X-Forwarded-For", out StringValues value))
            return value!;

        var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;
        return remoteIpAddress is null ? DefaultIpAddress : remoteIpAddress.MapToIPv4().ToString();
    }
}