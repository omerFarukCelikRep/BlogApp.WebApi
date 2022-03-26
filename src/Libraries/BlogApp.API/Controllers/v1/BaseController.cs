using BlogApp.Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class BaseController : ControllerBase
{
    protected Core.Utilities.Results.Abstract.IResult PopulateError(string message)
    {
        return new ErrorResult(message);
    }
}
