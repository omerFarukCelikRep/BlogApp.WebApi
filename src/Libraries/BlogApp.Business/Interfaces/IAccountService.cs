using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.Entities.DbSets;

namespace BlogApp.Business.Interfaces;
public interface IAccountService
{
    Task<AuthResult> AddAsync(UserRegistrationRequestDto registrationRequestDto, CancellationToken cancellationToken = default);
    Task<AuthResult> AuthenticateAsync(UserLoginRequestDto loginRequestDto, string ipAddress, CancellationToken cancellationToken = default);
    Task<IDataResult<User>> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<AuthResult> RefreshTokenAsync(TokenRequestDto tokenRequestDto, CancellationToken cancellationToken = default);
}
