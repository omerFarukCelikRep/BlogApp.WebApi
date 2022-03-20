using BlogApp.Authentication.Configurations;
using BlogApp.Authentication.Dtos.Generic;
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

    public TokenService(IOptions<JwtConfig> options, IRefreshTokenRepository refreshTokenRepository)
    {
        _jwtConfig = options.Value;
        _refreshTokenRepository = refreshTokenRepository;
    }
    public async Task<TokenData> GenerateJwtToken(IdentityUser<Guid> user)
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

        await _refreshTokenRepository.AddAsync(refreshToken);

        var tokenData = new TokenData
        {
            JwtToken = jwtToken,
            RefreshToken = refreshToken.Token
        };

        return tokenData;
    }

    private string RandomStringGenerator(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVXYZ0123456789";

        return new string(Enumerable.Repeat(chars, length)
            .Select(a => a[random.Next(a.Length)]).ToArray());
    }
}
