using BlogApp.Authentication.Constants;
using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Authentication.Interfaces.Services;
using BlogApp.Authentication.Options;
using BlogApp.Core.Utilities.Constants;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.DbSets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlogApp.Authentication.Services;
public class TokenService : ITokenService
{
    private readonly JwtOptions _jwtOptions;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly JwtBearerOptions _jwtBearerOptions;
    public TokenService(IOptions<JwtOptions> options,
                        IRefreshTokenRepository refreshTokenRepository,
                        IOptions<JwtBearerOptions> jwtBearerOptions)
    {
        _jwtOptions = options.Value;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtBearerOptions = jwtBearerOptions.Value;
    }
    public string GenerateJwtToken(User user)
    {
        var claims = new Claim[]
       {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) //Used by the refreshed token
       };

        var signInCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret!)), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_jwtOptions.Issuer, _jwtOptions.Audience, claims, null, DateTime.Now.Add(_jwtOptions.ExpiryTimeFrame), signInCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<AuthResult> VerifyTokenAsync(TokenRequestDto tokenRequestDto, CancellationToken cancellationToken = default)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var validationResult = await tokenHandler.ValidateTokenAsync(tokenRequestDto.Token, _jwtBearerOptions.TokenValidationParameters);

            if (!ValidateEncryptionAlg(validationResult.SecurityToken))
                return new AuthResult(false, validationResult.Exception.Message);

            if (!CheckTokenExpired(validationResult.Claims))
                return new AuthResult(false, AuthenticationMessages.JWTTokenNotExpired);

            var refreshToken = await CheckRefreshTokenExist(tokenRequestDto.RefreshToken, cancellationToken);
            if (refreshToken == null)
                return new AuthResult(false, AuthenticationMessages.InvalidRefreshToken);

            if (refreshToken.IsExpired)
                return new AuthResult(false, AuthenticationMessages.UsedRefreshToken);

            if (refreshToken.IsRevoked)
                return new AuthResult(false, AuthenticationMessages.RevokedRefreshToken);

            return new AuthResult()
            {
                Success = true,
                Token = tokenRequestDto.Token,
                RefreshToken = tokenRequestDto.RefreshToken
            };
        }
        catch (Exception ex)
        {
            //TODO: Add logger
            return new AuthResult(false, ExceptionMessages.SomethingWentWrong, ex.Message);
        }
    }

    public async Task<RefreshToken> GenerateRefreshTokenAsync(User user, string ipAddress, CancellationToken cancellationToken = default)
    {
        var refreshToken = new RefreshToken()
        {
            Token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),
            ExpiryDate = DateTime.Now.AddDays(7),
            UserId = user.Id
        };

        var tokenIsNotUnique = await _refreshTokenRepository.AnyAsync(x => x.Token == refreshToken.Token, cancellationToken);
        return tokenIsNotUnique ? await GenerateRefreshTokenAsync(user, ipAddress, cancellationToken) : await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);
    }

    public async Task<Guid?> ValidateJwtTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(token))
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtOptions.Secret!);

        var validationResult = await tokenHandler.ValidateTokenAsync(token, _jwtBearerOptions.TokenValidationParameters);

        var jwtToken = validationResult?.SecurityToken;
        if (jwtToken is null or not JwtSecurityToken)
            return null;

        var userId = ((JwtSecurityToken)jwtToken).Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
            return null;

        bool parseResult = Guid.TryParse(userId, out Guid result);
        return parseResult ? result : null;
    }

    public async Task<bool> UpdateRefreshTokenAsUsedAsync(string token, CancellationToken cancellationToken = default)
    {
        var refreshToken = await _refreshTokenRepository.GetByRefreshTokenAsync(token, cancellationToken);
        if (refreshToken is null)
        {
            return false;
        }

        return await _refreshTokenRepository.UpdateRefreshTokenAsUsedAsync(refreshToken, cancellationToken);
    }

    private bool ValidateEncryptionAlg(SecurityToken validatedToken)
    {
        if (validatedToken is not JwtSecurityToken jwtSecurityToken)
            return true;

        var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

        return result;
    }

    private DateTime UnixTimeStampToDateTime(long unixDate)
    {
        DateTime dateTime = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
        dateTime = dateTime.AddSeconds(unixDate).ToUniversalTime();

        return dateTime;
    }

    private bool CheckTokenExpired(IDictionary<string, object> claims)
    {
        bool result = long.TryParse(claims.FirstOrDefault(x => x.Key == JwtRegisteredClaimNames.Exp).Value.ToString(), out long expiryUnixDate);
        if (!result)
            return result;

        var expiryDate = UnixTimeStampToDateTime(expiryUnixDate);

        return expiryDate <= DateTime.Now;
    }

    private Task<RefreshToken?> CheckRefreshTokenExist(string refreshToken, CancellationToken cancellationToken = default)
    {
        return _refreshTokenRepository.GetByRefreshTokenAsync(refreshToken, cancellationToken);
    }

    public Task<RefreshToken?> GetActiveRefreshTokenAsync(User user, CancellationToken cancellationToken = default)
    {
        return _refreshTokenRepository.GetAsync(x => x.UserId == user.Id, false, cancellationToken);
    }
}