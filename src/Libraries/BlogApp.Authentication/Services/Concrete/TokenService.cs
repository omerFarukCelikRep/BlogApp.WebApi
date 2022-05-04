using BlogApp.Authentication.Configurations;
using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Authentication.Services.Abstract;
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
            Expires = DateTime.UtcNow.Add(_jwtConfig.ExpiryTimeFrame), //TODO: Update expiration time
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature //TODO: Review the algorithm
            )
        };

            var token = jwtHandler.CreateToken(tokenDescriptor);

            return jwtHandler.WriteToken(token);
    }

    public async Task<AuthResult?> VerifyTokenAsync(TokenRequestDto tokenRequestDto)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var principal = tokenHandler.ValidateToken(tokenRequestDto.Token, _tokenValidationParameters, out var validatedToken);

            if (!ValidateEncryptionAlg(validatedToken))
            {
                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "JWT token has not expired" //TODO:Magic string
                    }
                };
            }

            if (!CheckTokenExpired(principal))
            {
                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "JWT token has not expired" //TODO:Magic string
                    }
                };
            }

            var refreshToken = await CheckRefreshTokenExist(tokenRequestDto.RefreshToken);

            if (refreshToken == null)
            {
                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Invalid Refresh Token" //TODO:Magic string
                    }
                };
            }

            if (refreshToken.IsExpired)
            {
                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Token has been used" //TODO:Magic string
                    }
                };
            }

            if (refreshToken.IsRevoked)
            {
                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Refresh Token has been revoked, it cannot be used"  //TODO:Magic string
                    }
                };
            }

            if (refreshToken.IsExpired)
            {
                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Refresh token has expired" //TODO:Magic string
                    },

                };
            }

            var markasUsed = await _refreshTokenRepository.UpdateRefreshTokenAsUsed(refreshToken);

            if (!markasUsed)
            {
                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Something went wrong" //TODO:Magic string
                    }
                };
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
            if (ex.Message.Contains("Lifetime validation failed. The token is expired."))
            {

                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Token has expired please re-login"
                    }
                };

            }
            return new AuthResult()
            {
                Success = false,
                Errors = new List<string>()
                {
                    "Something went wrong."
                }
            };

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

        return refreshToken;
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

    private bool CheckTokenExpired(ClaimsPrincipal principal)
    {
        var expiryUnixDate = long.Parse(principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

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
