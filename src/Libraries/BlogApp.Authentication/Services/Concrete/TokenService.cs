using BlogApp.Authentication.Configurations;
using BlogApp.Authentication.Constants;
using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Authentication.Services.Abstract;
using BlogApp.Core.Utilities.Constants;
using BlogApp.DataAccess.Abstract;
using BlogApp.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlogApp.Authentication.Services.Concrete;
public class TokenService : ITokenService
{
    private readonly JwtConfig _jwtConfig;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly TokenValidationParameters _tokenValidationParameters;
    public TokenService(IOptions<JwtConfig> options,
                        IRefreshTokenRepository refreshTokenRepository,
                        TokenValidationParameters tokenValidationParameters)
    {
        _jwtConfig = options.Value;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenValidationParameters = tokenValidationParameters;
    }
    public string GenerateJwtToken(IdentityUser<Guid> user)
    {
        //The handler is going to be responsible for creating the token
        var jwtHandler = new JwtSecurityTokenHandler();

        //Get security key
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) //Used by the refreshed token

                }),
            Expires = DateTime.Now.Add(_jwtConfig.ExpiryTimeFrame), //TODO: Update expiration time
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
            var validationResult = await tokenHandler.ValidateTokenAsync(tokenRequestDto.Token, _tokenValidationParameters);

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
            UserId = user.Id,
            User = user
        };

        var tokenIsNotUnique = await _refreshTokenRepository.AnyAsync(x => x.Token == refreshToken.Token);

        if (tokenIsNotUnique)
        {
            return await GenerateRefreshTokenAsync(user, ipAddress);
        }

        return await _refreshTokenRepository.AddAsync(refreshToken);
    }

    public async Task<RefreshToken> GetActiveRefreshTokenAsync(IdentityUser<Guid> user)
    {
        var refreshTokens = await _refreshTokenRepository.GetAllAsync(x => x.UserId == user.Id, false);

        return refreshTokens.Single(x => x.IsActive);
    }

    public async Task<Guid?> ValidateJwtTokenAsync(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtConfig.Secret);

        try
        {
            var validationResult = await tokenHandler.ValidateTokenAsync(token, _tokenValidationParameters);

            var jwtToken = (JwtSecurityToken)validationResult.SecurityToken;
            return Guid.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "Id").Value);
        }
        catch (Exception ex)
        {
            //TODO:Add Logger
            return null;
        }
    }

    public async Task<bool> UpdateRefreshTokenAsUsedAsync(string token)
    {
        var refreshToken = await _refreshTokenRepository.GetByRefreshTokenAsync(token);

        return await _refreshTokenRepository.UpdateRefreshTokenAsUsed(refreshToken);
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
        var expiryUnixDate = long.Parse(claims.FirstOrDefault(x => x.Key == JwtRegisteredClaimNames.Exp).Value.ToString());

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