using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogApp.API.Controllers.v1;
public class AccountsController : BaseController
{
    private const int DefaultCookieExpireTime = 30;
    private const string RememberMeKey = "RememberMe";

    private readonly IAccountService _accountService;
    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto registrationRequestDto, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        registrationRequestDto.IpAddress = GetIpAddress();
        var registerResult = await _accountService.AddAsync(registrationRequestDto, cancellationToken);
        return !registerResult.Success ? BadRequest(registerResult) : Ok(registerResult);
    }

    [HttpPost("Authenticate")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] UserLoginRequestDto loginRequestDto, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var authenticationResult = await _accountService.AuthenticateAsync(loginRequestDto, GetIpAddress(), cancellationToken);
        if (!authenticationResult.Success)
            return Unauthorized(authenticationResult);

        if (loginRequestDto.RememberMe)
            SetRememberMe(loginRequestDto.Email);

        return Ok(authenticationResult);
    }

    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDto tokenRequestDto, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        tokenRequestDto.IpAddress = GetIpAddress();
        var result = await _accountService.RefreshTokenAsync(tokenRequestDto, cancellationToken);
        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public Task<IActionResult> Logout(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        RemoveRememberMe();
        return Task.FromResult((IActionResult)NoContent());
    }

    private void SetRememberMe(string email)
    {
        var options = new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(DefaultCookieExpireTime)
        };

        Response.Cookies.Append(RememberMeKey, email, options);
    }

    private void RemoveRememberMe() => Response.Cookies.Delete(RememberMeKey);
}