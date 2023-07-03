using BlogApp.Authentication.Constants;
using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Authentication.Interfaces.Providers;
using BlogApp.Authentication.Interfaces.Services;
using BlogApp.Business.Constants;
using BlogApp.Business.Interfaces;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Authentication;
using BlogApp.Core.Utilities.Constants;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.DbSets;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Text.RegularExpressions;

namespace BlogApp.Business.Services;
public class AccountService : IAccountService
{
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IJwtProvider _jwtProvider;
    public AccountService(UserManager<IdentityUser<Guid>> userManager, IUserRepository userRepository, ITokenService tokenService, IJwtProvider jwtProvider)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _tokenService = tokenService;
        _jwtProvider = jwtProvider;
    }
    public async Task<AuthResult> AddAsync(UserRegistrationRequestDto registrationRequestDto)
    {
        var existUser = await _userRepository.GetByEmailAsync(registrationRequestDto.Email);
        if (existUser is not null)
            return new AuthResult(false, AuthenticationMessages.EmailAlredyTaken);

        if (!ValidatePassword(registrationRequestDto.Password))
            return new AuthResult(false, AuthenticationMessages.PasswordIsNotValid);

        if (registrationRequestDto.Password != registrationRequestDto.ConfirmedPassword)
            return new AuthResult(false, AuthenticationMessages.PasswordMustMatch);

        var user = ObjectMapper.Mapper.Map<User>(registrationRequestDto);
        var (salt,hash) = PasswordHelper.HashPassword(registrationRequestDto.Password);
        user.PasswordSalt = salt;
        user.PasswordHash = hash;

        await _userRepository.AddAsync(user);

        var jwtToken = _jwtProvider.Generate(identityCreateResult.Data, user.Id);

        //var jwtToken = _tokenService.GenerateJwtToken(identityCreateResult.Data, member.Id);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(identityCreateResult.Data, registrationRequestDto.IpAddress);
        await _userRepository.SaveChangesAsync();

        return new AuthResult
        {
            Success = true,
            Token = jwtToken,
            RefreshToken = refreshToken.Token!
        };
    }

    public async Task<AuthResult> AuthenticateAsync(UserLoginRequestDto loginRequestDto, string ipAddress)
    {
        var identityUser = await _userManager.FindByEmailAsync(loginRequestDto.Email);
        if (identityUser is null)
        {
            return new AuthResult(false, AuthenticationMessages.InvalidRequest);
        }

        var checkPasswordResult = await _userManager.CheckPasswordAsync(identityUser, loginRequestDto.Password);
        if (!checkPasswordResult)
        {
            return new AuthResult(false, AuthenticationMessages.InvalidRequest);
        }

        var user = await _userRepository.GetByEmailAsync(loginRequestDto.Email);
        if (user is null)
        {
            return new AuthResult(false, AuthenticationMessages.InvalidRequest);
        }

        var jwtToken = _jwtProvider.Generate(identityUser, user.Id);
        var refreshToken = await _tokenService.GetActiveRefreshTokenAsync(identityUser)
                           ?? await _tokenService.GenerateRefreshTokenAsync(identityUser, ipAddress);

        return new AuthResult
        {
            Success = true,
            Token = jwtToken,
            RefreshToken = refreshToken.Token!
        };
    }

    public async Task<IDataResult<IdentityUser<Guid>>> FindByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user == null ? new ErrorDataResult<IdentityUser<Guid>>(ServiceMessages.UserNotFound) : new SuccessDataResult<IdentityUser<Guid>>(user); //TODO:Magic string
    }

    public async Task<AuthResult> RefreshTokenAsync(TokenRequestDto tokenRequestDto)
    {
        var identityUserId = await _tokenService.ValidateJwtTokenAsync(tokenRequestDto.Token);
        if (identityUserId is null)
        {
            return new AuthResult(false, ExceptionMessages.SomethingWentWrong);
        }

        var verifyResult = await _tokenService.VerifyTokenAsync(tokenRequestDto);
        if (!verifyResult.Success)
        {
            return verifyResult;
        }

        bool markedAsUsed = await _tokenService.UpdateRefreshTokenAsUsedAsync(tokenRequestDto.RefreshToken);
        if (!markedAsUsed)
        {
            return new AuthResult(false, ExceptionMessages.SomethingWentWrong);
        }

        var user = await _userManager.FindByIdAsync(identityUserId.ToString()!);
        var member = await _userRepository.GetByEmailAsync(user.Email);
        var jwtToken = _jwtProvider.Generate(user!, member!.Id);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user!, tokenRequestDto.IpAddress);

        return new AuthResult
        {
            Success = true,
            Token = jwtToken,
            RefreshToken = refreshToken.Token!
        };
    }

    private async Task<User> AddMember(UserRegistrationRequestDto registrationRequestDto, Guid identityId)
    {
        var member = ObjectMapper.Mapper.Map<User>(registrationRequestDto);

        return await _userRepository.AddAsync(member);
    }


    private bool ValidatePassword(string password)
    {
        var regex = new Regex("""^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=|\]).{8,32}$""");

        return regex.IsMatch(password);
    }
}
