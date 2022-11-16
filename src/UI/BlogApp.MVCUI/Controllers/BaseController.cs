using BlogApp.MVCUI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVCUI.Controllers;

[AuthorizationFilter]
public class BaseController : Controller
{
}
