using BlogApp.Authentication.Constants;
using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers.v1;
public class AccountsController : BaseController
{
    private readonly IUserService _userService;

    public AccountsController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto registrationRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userExistingResult = await _userService.FindByEmailAsync(registrationRequestDto.Email);

        if (userExistingResult.IsSuccess)
        {
            return BadRequest(new AuthResult(false, AuthenticationMessages.EmailAlredyTaken));
        }

        registrationRequestDto.IpAddress = GetIpAddress();

        var registerResult = await _userService.AddAsync(registrationRequestDto);

        if (!registerResult.Success)
        {
            return BadRequest(registerResult);
        }

        return Ok(registerResult);
    }

    [HttpPost]
    [Route("Authenticate")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] UserLoginRequestDto loginRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var authenticationResult = await _userService.AuthenticateAsync(loginRequestDto, GetIpAddress());

        if (!authenticationResult.Success)
        {
            return Unauthorized(authenticationResult);
        }

        return Ok(authenticationResult);
    }

    [HttpPost]
    [Route("RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDto tokenRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        tokenRequestDto.IpAddress = GetIpAddress();

        var result = await _userService.RefreshTokenAsync(tokenRequestDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    private string GetIpAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        else
            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }
}
