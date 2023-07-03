using BlogApp.Authentication.Constants;
using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers.v1;
public class AccountsController : BaseController
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
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
        
        registrationRequestDto.IpAddress = GetIpAddress();
        var registerResult = await _accountService.AddAsync(registrationRequestDto);
        return !registerResult.Success ? BadRequest(registerResult) : Ok(registerResult);
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

        var authenticationResult = await _accountService.AuthenticateAsync(loginRequestDto, GetIpAddress());
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
        var result = await _accountService.RefreshTokenAsync(tokenRequestDto);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
