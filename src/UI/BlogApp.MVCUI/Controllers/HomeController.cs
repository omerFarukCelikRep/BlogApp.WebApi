using BlogApp.MVCUI.Models.Authentication;
using BlogApp.MVCUI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVCUI.Controllers;

[AllowAnonymous]
public class HomeController : BaseController
{
    private readonly IIdentityService _identityService;

    public HomeController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [Route("Login")]
    public IActionResult Login(string returnUrl)
    {
        TempData["returnUrl"] = returnUrl;
        if (User.Identity!.IsAuthenticated)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    [HttpPost]
    [Route("Login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        if (!ModelState.IsValid)
        {
            return View(loginVM);
        }

        var result = await _identityService.LoginAsync(loginVM);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message!);
            return View(loginVM);
        }
        var returnUrl = TempData["returnUrl"]?.ToString();
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return LocalRedirect(returnUrl);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Route("Register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [Route("Register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVM registerVM)
    {
        if (!ModelState.IsValid)
        {
            return View(registerVM);
        }

        var result = await _identityService.RegisterAsync(registerVM);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message!);
            return View(registerVM);
        }

        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        await _identityService.SignOutAsync();

        return RedirectToAction(nameof(Index));
    }
}
