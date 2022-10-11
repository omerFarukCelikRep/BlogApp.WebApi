using BlogApp.Authentication.Constants;
using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Authentication.Services.Abstract;
using BlogApp.Business.Constants;
using BlogApp.Business.Interfaces;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Constants;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace BlogApp.Business.Concrete;
public class UserService : IUserService
{
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    private readonly IMemberRepository _memberRepository;
    private readonly ITokenService _tokenService;

    public UserService(UserManager<IdentityUser<Guid>> userManager, IMemberRepository memberRepository, ITokenService tokenService)
    {
        _userManager = userManager;
        _memberRepository = memberRepository;
        _tokenService = tokenService;
    }
    public async Task<AuthResult> AddAsync(UserRegistrationRequestDto registrationRequestDto)
    {
        var identityCreateResult = await AddIdentityUser(registrationRequestDto);
        if (!identityCreateResult.IsSuccess)
        {
            return new AuthResult(false, identityCreateResult.Message);
        }

        var member = await AddMember(registrationRequestDto, identityCreateResult.Data.Id);

        var jwtToken = _tokenService.GenerateJwtToken(identityCreateResult.Data, member.Id);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(identityCreateResult.Data, registrationRequestDto.IpAddress);
        await _memberRepository.SaveChangesAsync();

        return new AuthResult
        {
            Success = true,
            Token = jwtToken,
            RefreshToken = refreshToken.Token
        };
    }

    public async Task<AuthResult> AuthenticateAsync(UserLoginRequestDto loginRequestDto, string ipAddress)
    {
        var findUserResult = await FindByEmailAsync(loginRequestDto.Email);
        if (!findUserResult.IsSuccess)
        {
            return new AuthResult(false, AuthenticationMessages.InvalidRequest);
        }

        var checkPasswordResult = await _userManager.CheckPasswordAsync(findUserResult.Data, loginRequestDto.Password);
        if (!checkPasswordResult)
        {
            return new AuthResult(false, AuthenticationMessages.InvalidRequest);
        }

        var member = await _memberRepository.GetByIdentityId(findUserResult.Data.Id);

        var jwtToken = _tokenService.GenerateJwtToken(findUserResult.Data, member.Id);
        var refreshToken = await _tokenService.GetActiveRefreshTokenAsync(findUserResult.Data)
                           ?? await _tokenService.GenerateRefreshTokenAsync(findUserResult.Data, ipAddress);

        return new AuthResult
        {
            Success = true,
            Token = jwtToken,
            RefreshToken = refreshToken.Token
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

        var user = await _userManager.FindByIdAsync(identityUserId.ToString());
        var member = await _memberRepository.GetByIdentityId(identityUserId.Value);
        var jwtToken = _tokenService.GenerateJwtToken(user, member.Id);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user, tokenRequestDto.IpAddress);

        return new AuthResult
        {
            Success = true,
            Token = jwtToken,
            RefreshToken = refreshToken.Token
        };
    }

    public async Task<Guid> GetUserIdByIdentityIdAsync(Guid identityId)
    {
        return (await _memberRepository.GetByIdentityId(identityId)).Id;
    }

    private async Task<Member> AddMember(UserRegistrationRequestDto registrationRequestDto, Guid identityId)
    {
        var member = ObjectMapper.Mapper.Map<Member>(registrationRequestDto);

        member.IdentityId = identityId;

        return await _memberRepository.AddAsync(member);
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
