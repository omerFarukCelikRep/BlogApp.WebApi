using BlogApp.Core.Utilities.Results.Abstract;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.MVCUI.Models;
using BlogApp.MVCUI.Services.Interfaces;
using BlogApp.MVCUI.Services.Results;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace BlogApp.MVCUI.Services.Concretes;

public class IdentityService : IIdentityService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsLoggedIn => !string.IsNullOrEmpty(GetUserToken());

    public string GetUserToken()
    {
        return _httpContextAccessor.HttpContext.Session.GetString("Token");
    }

    public async Task<Core.Utilities.Results.Abstract.IResult> LoginAsync(LoginVM loginVM)
    {
        var responseMessage = await _httpClient.PostAsJsonAsync("/api/v1/Authenticate", loginVM);
        if (responseMessage is not null && !responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadFromJsonAsync<AuthResult>();
            if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                return new ErrorResult("Giriş Başarısız"); //TODO: Magic string
            }

            _httpContextAccessor.HttpContext?.Session.SetString("Token", response.Token);
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("RefreshToken", response.RefreshToken);

            return new SuccessResult("Giriş Başarılı"); //TODO: Magic string
        }

        return new ErrorResult("Giriş Başarısız"); //TODO: Magic string
    }
}
