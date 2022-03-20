using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Authentication.Services.Abstract;
using BlogApp.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers.v1;
public class AccountsController : BaseController
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public AccountsController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto registrationRequestDto)
    {
        var userExistingResult = await _userService.FindByEmailAsync(registrationRequestDto.Email);

        if (userExistingResult.IsSuccess)
        {
            return BadRequest(new UserRegistrationResponseDto()
            {
                Success = false,
                Errors = new()
                {
                    "Email already taken" //TODO: Magic string
                }
            });
        }

        var registerResult = await _userService.AddAsync(registrationRequestDto);

        if (!registerResult.IsSuccess)
        {
            return BadRequest(new UserRegistrationResponseDto()
            {
                Success = registerResult.IsSuccess,
                Errors = new()
                {
                    registerResult.Message
                }
            });
        }

        return Ok(new UserRegistrationResponseDto()
        {
            Success = registerResult.IsSuccess,
            RefreshToken = registerResult.Data.RefreshToken,
            Token = registerResult.Data.JwtToken
        });
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto loginRequestDto)
    {
        var userExistingResult = await _userService.FindByEmailAsync(loginRequestDto.Email);

        if (!userExistingResult.IsSuccess)
        {
            return BadRequest(new UserLoginResponseDto()
            {
                Success = userExistingResult.IsSuccess,
                Errors = new List<string>()
                {
                    "Invalid authentication request",
                    userExistingResult.Message
                }

            });
        }

        var checkPasswordResult = await _userService.CheckPasswordAsync(userExistingResult.Data, loginRequestDto.Password);

        if (!checkPasswordResult.IsSuccess)
        {
            return BadRequest(new UserLoginResponseDto()
            {
                Success = userExistingResult.IsSuccess,
                Errors = new List<string>()
                {
                    "Invalid authentication request"
                }

            });
        }

        var jwtToken = await _tokenService.GenerateJwtToken(userExistingResult.Data);

        return Ok(new UserLoginResponseDto()
        {
            Success = checkPasswordResult.IsSuccess,
            Token = jwtToken.JwtToken,
            RefreshToken = jwtToken.RefreshToken
        });
    }
}
