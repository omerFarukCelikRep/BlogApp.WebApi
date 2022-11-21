using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVCUI.Controllers;

[Authorize]
public class BaseController : Controller
{
}
