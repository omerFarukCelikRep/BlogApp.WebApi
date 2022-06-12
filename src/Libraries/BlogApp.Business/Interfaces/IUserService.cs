using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Core.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Business.Interfaces;
public interface IUserService
{
    Task<AuthResult> AddAsync(UserRegistrationRequestDto registrationRequestDto);
    Task<AuthResult> AuthenticateAsync(UserLoginRequestDto loginRequestDto, string ipAddress);
    Task<IDataResult<IdentityUser<Guid>>> FindByEmailAsync(string email);
    Task<AuthResult> RefreshTokenAsync(TokenRequestDto tokenRequestDto);
}
