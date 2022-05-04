using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Authentication.Services.Abstract;
using BlogApp.Business.Abstract;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Results.Abstract;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.DataAccess.Abstract;
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
            return new AuthResult(string.Empty,string.Empty, false, identityCreateResult.Message);
        }

        _ = await AddMember(registrationRequestDto, identityCreateResult.Data.Id);

        var jwtToken = _tokenService.GenerateJwtToken(identityCreateResult.Data);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(identityCreateResult.Data, registrationRequestDto.IpAddress);

        var authResult = new AuthResult
        {
            Success = true,
            Token = jwtToken,
            RefreshToken = refreshToken.Token
        };

        return authResult;
    }

    public async Task<AuthResult> AuthenticateAsync(UserLoginRequestDto loginRequestDto, string ipAddress)
    {
        var findUserResult = await FindByEmailAsync(loginRequestDto.Email);
        if (!findUserResult.IsSuccess)
        {
            return new AuthResult(string.Empty, string.Empty, false, "Invalid authentication request");
        }

        var checkPasswordResult = await _userManager.CheckPasswordAsync(findUserResult.Data, loginRequestDto.Password);
        if (!checkPasswordResult)
        {
            return new AuthResult(string.Empty,string.Empty, false, "Invalid authentication request");
        }

        var jwtToken = _tokenService.GenerateJwtToken(findUserResult.Data);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(findUserResult.Data, ipAddress);

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
        return user == null ? new ErrorDataResult<IdentityUser<Guid>>("Kullanıcı Bulunamadı") : new SuccessDataResult<IdentityUser<Guid>>(user);
    }

    public async Task<AuthResult> RefreshTokenAsync(TokenRequestDto tokenRequestDto)
    {
        var userId = await _tokenService.ValidateJwtTokenAsync(tokenRequestDto.Token);

        var verifyResult = await _tokenService.VerifyTokenAsync(tokenRequestDto);

        if (!verifyResult.Success)
        {
            return verifyResult;
        }

        _ = await _tokenService.UpdateRefreshTokenAsUsedAsync(tokenRequestDto.RefreshToken);

        var user = await _userManager.FindByIdAsync(userId.ToString());

        var jwtToken = _tokenService.GenerateJwtToken(user);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user, tokenRequestDto.IpAddress);

        return new AuthResult
        {
            Success = true,
            Token = jwtToken,
            RefreshToken = refreshToken.Token
        };
    }

    private async Task<Member> AddMember(UserRegistrationRequestDto registrationRequestDto, Guid identityId)
    {
        var member = ObjectMapper.Mapper.Map<Member>(registrationRequestDto);

        member.IdentityId = identityId;

        return await _memberRepository.AddAsync(member);
    }

    private async Task<IDataResult<IdentityUser<Guid>>> AddIdentityUser(UserRegistrationRequestDto registrationRequestDto)
    {
        var newUser = ObjectMapper.Mapper.Map<IdentityUser<Guid>>(registrationRequestDto); //TODO: Build functionality to send user to confirm email

        var isCreated = await _userManager.CreateAsync(newUser, registrationRequestDto.Password);

        if (!isCreated.Succeeded)
        {
            return new ErrorDataResult<IdentityUser<Guid>>(ConcatIdentityErrors(isCreated.Errors));
        }

        return new SuccessDataResult<IdentityUser<Guid>>(newUser);
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
