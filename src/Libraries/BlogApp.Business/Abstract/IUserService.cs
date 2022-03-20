using BlogApp.Authentication.Dtos.Generic;
using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Core.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Business.Abstract;
public interface IUserService
{
    Task<IDataResult<TokenData>> AddAsync(UserRegistrationRequestDto registrationRequestDto);
    Task<IResult> CheckPasswordAsync(IdentityUser<Guid> user, string password);
    Task<IDataResult<IdentityUser<Guid>>> FindByEmailAsync(string email);
}
