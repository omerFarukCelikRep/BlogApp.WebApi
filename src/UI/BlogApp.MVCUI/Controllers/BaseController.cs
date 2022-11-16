using BlogApp.MVCUI.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVCUI.Controllers;

[AuthorizationFilter]
public class BaseController : Controller
{
}
