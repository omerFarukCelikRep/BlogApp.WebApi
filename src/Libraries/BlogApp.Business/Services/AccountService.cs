using BlogApp.Authentication.Constants;
using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Authentication.Interfaces.Services;
using BlogApp.Business.Interfaces;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Authentication;
using BlogApp.Core.Utilities.Constants;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.DbSets;
using System.Text.RegularExpressions;
using UserMessages = BlogApp.Business.Constants.ServiceMessages.User;

namespace BlogApp.Business.Services;
public partial class AccountService(IUserRepository userRepository,
                            IUserSessionRepository userSessionRepository,
                            ITokenService tokenService)
    : IAccountService
{
    public async Task<AuthResult> AddAsync(UserRegistrationRequestDto registrationRequestDto, CancellationToken cancellationToken = default)
    {
        var existUser = await GetByEmailAsync(registrationRequestDto.Email, cancellationToken);
        if (existUser is not null)
            return new AuthResult(false, AuthenticationMessages.EmailAlredyTaken);

        if (!ValidatePassword(registrationRequestDto.Password))
            return new AuthResult(false, AuthenticationMessages.PasswordIsNotValid);

        var user = ObjectMapper.Mapper.Map<User>(registrationRequestDto);
        var (salt, hash) = PasswordHelper.HashPassword(registrationRequestDto.Password);
        user.PasswordSalt = salt;
        user.PasswordHash = hash;

        await userRepository.AddAsync(user, cancellationToken);

        var jwtToken = await tokenService.GenerateJwtToken(user, cancellationToken: cancellationToken);
        var refreshToken = await tokenService.GenerateRefreshTokenAsync(user, registrationRequestDto.IpAddress, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);

        return new AuthResult(success: true, token: jwtToken, refreshToken: refreshToken.Token!);
    }

    public async Task<AuthResult> AuthenticateAsync(UserLoginRequestDto loginRequestDto, string ipAddress, CancellationToken cancellationToken = default)
    {
        var user = await GetByEmailAsync(loginRequestDto.Email, cancellationToken);
        if (user is null)
            return new AuthResult(false, AuthenticationMessages.InvalidRequest);

        var checkPasswordResult = PasswordHelper.VerifyPassword(loginRequestDto.Password, user.PasswordHash, user.PasswordSalt);
        if (!checkPasswordResult)
            return new AuthResult(false, AuthenticationMessages.InvalidRequest);

        var jwtToken = await tokenService.GenerateJwtToken(user, loginRequestDto.RememberMe, cancellationToken);
        var refreshToken = await tokenService.GetActiveRefreshTokenAsync(user, cancellationToken)
                           ?? await tokenService.GenerateRefreshTokenAsync(user, ipAddress, cancellationToken);

        var userSession = new UserSession
        {
            ExpireDate = loginRequestDto.RememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddMinutes(30),
            IpAddress = ipAddress,
            Token = jwtToken,
            UserId = user.Id,
        };
        await userSessionRepository.AddAsync(userSession, cancellationToken);

        return new AuthResult(success: true, token: jwtToken, refreshToken: refreshToken.Token!);
    }

    public async Task<IResult<User?>> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await GetByEmailAsync(email, cancellationToken);
        return user is null ? Result<User>.Failure(new Error(string.Empty, UserMessages.NotFound)) : Result<User>.Success(user);
    }

    public async Task<AuthResult> RefreshTokenAsync(TokenRequestDto tokenRequestDto, CancellationToken cancellationToken = default)
    {
        var userId = await tokenService.ValidateJwtTokenAsync(tokenRequestDto.Token, cancellationToken);
        if (userId is null)
            return new AuthResult(false, ExceptionMessages.SomethingWentWrong);

        var verifyResult = await tokenService.VerifyTokenAsync(tokenRequestDto, cancellationToken);
        if (!verifyResult.Success)
            return verifyResult;

        bool markedAsUsed = await tokenService.UpdateRefreshTokenAsUsedAsync(tokenRequestDto.RefreshToken, cancellationToken);
        if (!markedAsUsed)
            return new AuthResult(false, ExceptionMessages.SomethingWentWrong);

        var user = await userRepository.GetByIdAsync(userId.Value, false, cancellationToken);
        if (user is null)
            return new AuthResult(false, UserMessages.NotFound);

        var jwtToken = await tokenService.GenerateJwtToken(user, cancellationToken: cancellationToken);
        var refreshToken = await tokenService.GenerateRefreshTokenAsync(user, tokenRequestDto.IpAddress, cancellationToken);

        return new(success: true, token: jwtToken, refreshToken: refreshToken.Token!);
    }

    private Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) => userRepository.GetByEmailAsync(email, false, cancellationToken);
    private static bool ValidatePassword(string password) => ValidatePassword().IsMatch(password);

    [GeneratedRegex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*+-]).{8,32}$")]
    private static partial Regex ValidatePassword();
}
