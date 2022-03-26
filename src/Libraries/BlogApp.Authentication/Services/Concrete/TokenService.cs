using BlogApp.Authentication.Configurations;
using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Authentication.Dtos.Outgoing;
using BlogApp.Authentication.Services.Abstract;
using BlogApp.Core.Entities.Enums;
using BlogApp.DataAccess.Abstract;
using BlogApp.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogApp.Authentication.Services.Concrete;
public class TokenService : ITokenService
{
    private readonly JwtConfig _jwtConfig;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly UserManager<IdentityUser<Guid>> _userManager;

    public TokenService(IOptions<JwtConfig> options,
                        IRefreshTokenRepository refreshTokenRepository,
                        TokenValidationParameters tokenValidationParameters,
                        UserManager<IdentityUser<Guid>> userManager)
    {
        _jwtConfig = options.Value;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenValidationParameters = tokenValidationParameters;
        _userManager = userManager;
    }
    public async Task<AuthResult> GenerateJwtToken(IdentityUser<Guid> user)
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

        //Generate the security token
        var token = jwtHandler.CreateToken(tokenDescriptor);

        //Convert the security obj token into a string
        var jwtToken = jwtHandler.WriteToken(token);

        //Generate a refresh token
        var refreshToken = new RefreshToken
        {
            CreatedDate = DateTime.UtcNow,
            Token = $"{RandomStringGenerator(25)}_{Guid.NewGuid()}",
            UserId = user.Id,
            IsRevoked = false,
            IsUsed = false,
            Status = Status.Added,
            JwtId = token.Id,
            ExpiryDate = DateTime.UtcNow.AddMonths(6)
        };

        _ = await _refreshTokenRepository.AddAsync(refreshToken);

        var result = new AuthResult()
        {
            Success = true,
            Token = jwtToken,
            RefreshToken = refreshToken.Token
        };

        return result;
    }

    public async Task<AuthResult?> VerifyToken(TokenRequestDto tokenRequestDto)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var principal = tokenHandler.ValidateToken(tokenRequestDto.Token, _tokenValidationParameters, out var validatedToken);

            if (!ValidateEncryptionAlg(validatedToken)) return null;

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

            if (refreshToken.IsUsed)
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

            var jti = principal.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            if (refreshToken.JwtId != jti)
            {
                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>()
                        {
                            "Refresh Token reference does not match the Jwt Token"  //TODO:Magic string
                        }
                };
            }

            if (refreshToken.ExpiryDate < DateTime.Now)
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

    public async Task<AuthResult> UpdateToken(string refreshToken)
    {
        var token = await CheckRefreshTokenExist(refreshToken);
        var user = await _userManager.FindByIdAsync(token.UserId.ToString());

        return await GenerateJwtToken(user);
    }

    private string RandomStringGenerator(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVXYZ0123456789";

        return new string(Enumerable.Repeat(chars, length)
            .Select(a => a[random.Next(a.Length)]).ToArray());
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

        dateTime.AddSeconds(unixDate).ToLocalTime();

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
