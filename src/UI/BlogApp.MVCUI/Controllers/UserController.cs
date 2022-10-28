using BlogApp.MVCUI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVCUI.Controllers;
public class UserController : BaseController
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult Main()
    {
        return View();
    }
}
