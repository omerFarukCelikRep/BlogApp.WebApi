using BlogApp.Authentication.Configurations;
using BlogApp.Authentication.Constants;
using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Authentication.Interfaces.Services;
using BlogApp.Core.Utilities.Constants;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.DbSets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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
    //private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly JwtBearerOptions _jwtBearerOptions;
    public TokenService(IOptions<JwtOptions> options,
                        IRefreshTokenRepository refreshTokenRepository,
                        //TokenValidationParameters tokenValidationParameters,
                        IOptions<JwtBearerOptions> jwtBearerOptions)
    {
        _jwtOptions = options.Value;
        _refreshTokenRepository = refreshTokenRepository;
        //_tokenValidationParameters = tokenValidationParameters;
        _jwtBearerOptions = jwtBearerOptions.Value;
    }
    public string GenerateJwtToken(IdentityUser<Guid> identityUser, Guid userId)
    {
        var jwtHandler = new JwtSecurityTokenHandler();

        //Get security key
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim("Id", userId.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, identityUser.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email!),
                    new Claim(JwtRegisteredClaimNames.Email, identityUser.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) //Used by the refreshed token
            }),
            Expires = DateTime.Now.Add(_jwtOptions.ExpiryTimeFrame), //TODO: Update expiration time
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature //TODO: Review the algorithm
            )
        };

        var token = jwtHandler.CreateToken(tokenDescriptor);

        return jwtHandler.WriteToken(token);
    }

    public async Task<AuthResult> VerifyTokenAsync(TokenRequestDto tokenRequestDto)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var validationResult = await tokenHandler.ValidateTokenAsync(tokenRequestDto.Token, _jwtBearerOptions.TokenValidationParameters);

            if (!ValidateEncryptionAlg(validationResult.SecurityToken))
            {
                return new AuthResult(false, validationResult.Exception.Message);
            }

            if (!CheckTokenExpired(validationResult.Claims))
            {
                return new AuthResult(false, AuthenticationMessages.JWTTokenNotExpired);
            }

            var refreshToken = await CheckRefreshTokenExist(tokenRequestDto.RefreshToken);

            if (refreshToken == null)
            {
                return new AuthResult(false, AuthenticationMessages.InvalidRefreshToken);
            }

            if (refreshToken.IsExpired)
            {
                return new AuthResult(false, AuthenticationMessages.UsedRefreshToken);
            }

            if (refreshToken.IsRevoked)
            {
                return new AuthResult(false, AuthenticationMessages.RevokedRefreshToken);
            }

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

    public async Task<RefreshToken> GenerateRefreshTokenAsync(IdentityUser<Guid> user, string ipAddress)
    {
        var refreshToken = new RefreshToken()
        {
            Token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),
            ExpiryDate = DateTime.Now.AddDays(7),
            UserId = user.Id
        };

        var tokenIsNotUnique = await _refreshTokenRepository.AnyAsync(x => x.Token == refreshToken.Token);

        if (tokenIsNotUnique)
        {
            return await GenerateRefreshTokenAsync(user, ipAddress);
        }

        return await _refreshTokenRepository.AddAsync(refreshToken);
    }

    public async Task<RefreshToken?> GetActiveRefreshTokenAsync(IdentityUser<Guid> user)
    {
        var refreshTokens = await _refreshTokenRepository.GetAllAsync(x => x.UserId == user.Id, false);

        return refreshTokens.FirstOrDefault(x => x.IsActive);
    }

    public async Task<Guid?> ValidateJwtTokenAsync(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtOptions.Secret!);

        var validationResult = await tokenHandler.ValidateTokenAsync(token, _jwtBearerOptions.TokenValidationParameters);

        var jwtToken = (JwtSecurityToken)validationResult.SecurityToken;
        bool parseResult = Guid.TryParse(jwtToken.Claims.FirstOrDefault(x => x.Type == "Id")?.Value, out Guid result);
        if (!parseResult)
        {
            return null;
        }

        return result;
    }

    public async Task<bool> UpdateRefreshTokenAsUsedAsync(string token)
    {
        var refreshToken = await _refreshTokenRepository.GetByRefreshTokenAsync(token);
        if (refreshToken is null)
        {
            return false;
        }

        return await _refreshTokenRepository.UpdateRefreshTokenAsUsedAsync(refreshToken);
    }

    private bool ValidateEncryptionAlg(SecurityToken validatedToken)
    {
        if (validatedToken is JwtSecurityToken jwtSecurityToken)
        {
            var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

            if (!result)
            {
                return false;
            }
        }
        return true;
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
        {
            return result;
        }

        var expiryDate = UnixTimeStampToDateTime(expiryUnixDate);

        if (expiryDate > DateTime.Now)
        {
            return false;
        }

        return true;
    }

    private async Task<RefreshToken?> CheckRefreshTokenExist(string refreshToken)
    {
        var refreshTokenExist = await _refreshTokenRepository.GetByRefreshTokenAsync(refreshToken);

        if (refreshTokenExist == null)
        {
            return null;
        }

        return refreshTokenExist;
    }
}