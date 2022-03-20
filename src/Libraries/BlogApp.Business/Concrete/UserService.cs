using BlogApp.Authentication.Dtos.Generic;
using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Services.Abstract;
using BlogApp.Business.Abstract;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Results.Abstract;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.DataAccess.Abstract;
using BlogApp.Entities.Concrete;
using BlogApp.Entities.Dtos.Members;
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
    public async Task<IDataResult<TokenData>> AddAsync(UserRegistrationRequestDto registrationRequestDto)
    {
        var identityCreateResult = await AddIdentityUser(registrationRequestDto);

        if (!identityCreateResult.IsSuccess)
        {
            return new ErrorDataResult<TokenData>(identityCreateResult.Message);
        }

        var addMemberResult = await AddMember(registrationRequestDto, identityCreateResult.Data.Id);

        var token = await _tokenService.GenerateJwtToken(identityCreateResult.Data);

        return new SuccessDataResult<TokenData>(token, "Kullanıcı Eklendi"); //TODO : Magic string
    }

    public async Task<IDataResult<IdentityUser<Guid>>> FindByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user == null ? new ErrorDataResult<IdentityUser<Guid>>("Kullanıcı Bulunamadı") : new SuccessDataResult<IdentityUser<Guid>>(user);
    }

    public async Task<IResult> CheckPasswordAsync(IdentityUser<Guid> user, string password)
    {
        return new Result(await _userManager.CheckPasswordAsync(user, password));
    }

    private async Task<IDataResult<Member>> AddMember(UserRegistrationRequestDto registrationRequestDto, Guid identityId)
    {
        var member = ObjectMapper.Mapper.Map<Member>(registrationRequestDto);

        member.IdentityId = identityId; 

        return new SuccessDataResult<Member>(await _memberRepository.AddAsync(member));
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
