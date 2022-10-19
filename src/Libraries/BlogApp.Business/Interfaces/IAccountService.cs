using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Core.Utilities.Results.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Business.Interfaces;
public interface IAccountService
{
    Task<AuthResult> AddAsync(UserRegistrationRequestDto registrationRequestDto);
    Task<AuthResult> AuthenticateAsync(UserLoginRequestDto loginRequestDto, string ipAddress);
    Task<IDataResult<IdentityUser<Guid>>> FindByEmailAsync(string email);
    Task<Guid> GetUserIdByIdentityIdAsync(Guid identityId);
    Task<AuthResult> RefreshTokenAsync(TokenRequestDto tokenRequestDto);
}
