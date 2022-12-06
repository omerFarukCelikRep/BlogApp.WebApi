using BlogApp.Authentication.Constants;
using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Authentication.Interfaces.Providers;
using BlogApp.Authentication.Interfaces.Services;
using BlogApp.Business.Constants;
using BlogApp.Business.Interfaces;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Constants;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.DbSets;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace BlogApp.Business.Concrete;
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
        var identityCreateResult = await AddIdentityUser(registrationRequestDto);
        if (!identityCreateResult.IsSuccess)
        {
            return new AuthResult(false, identityCreateResult.Message!);
        }

        var user = await AddMember(registrationRequestDto, identityCreateResult.Data!.Id);

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

        var user = await _userRepository.GetByIdentityId(identityUser.Id);
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
        var member = await _userRepository.GetByIdentityId(identityUserId.Value);
        var jwtToken = _jwtProvider.Generate(user!, member!.Id);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user!, tokenRequestDto.IpAddress);

        return new AuthResult
        {
            Success = true,
            Token = jwtToken,
            RefreshToken = refreshToken.Token!
        };
    }

    public async Task<Guid?> GetUserIdByIdentityIdAsync(Guid identityId)
    {
        return (await _userRepository.GetByIdentityId(identityId))?.Id;
    }

    private async Task<User> AddMember(UserRegistrationRequestDto registrationRequestDto, Guid identityId)
    {
        var member = ObjectMapper.Mapper.Map<User>(registrationRequestDto);

        member.IdentityId = identityId;

        return await _userRepository.AddAsync(member);
    }

    private async Task<IDataResult<IdentityUser<Guid>>> AddIdentityUser(UserRegistrationRequestDto registrationRequestDto)
    {
        var identityUser = new IdentityUser<Guid>
        {
            Email = registrationRequestDto.Email,
            EmailConfirmed = true, //TODO: EMail servisiyle beraber güncellenecek 
            UserName = registrationRequestDto.Email,

        };

        var isCreated = await _userManager.CreateAsync(identityUser, registrationRequestDto.Password);

        if (!isCreated.Succeeded)
        {
            return new ErrorDataResult<IdentityUser<Guid>>(ConcatIdentityErrors(isCreated.Errors));
        }

        return new SuccessDataResult<IdentityUser<Guid>>(identityUser);
    }

    private string ConcatIdentityErrors(IEnumerable<IdentityError> identityErrors)
    {
        StringBuilder stringBuilder = new();

        foreach (var identityError in identityErrors)
        {
            stringBuilder.Append(identityError.Code);
            stringBuilder.Append(" : ");
            stringBuilder.AppendLine(identityError.Description);
        }

        return stringBuilder.ToString();
    }
}
