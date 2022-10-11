using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.MVCUI.Models.Authentication;
using BlogApp.MVCUI.Services.Interfaces;
using BlogApp.MVCUI.Services.Results;
using System.Net;
using System.Text;
using IResult = BlogApp.Core.Utilities.Results.Interfaces.IResult;

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

    public async Task<IResult> LoginAsync(LoginVM loginVM)
    {
        var responseMessage = await _httpClient.PostAsJsonAsync("/api/v1/Accounts/Authenticate", loginVM);
        if (responseMessage is null)
        {
            return new ErrorResult("Giriş Başarısız"); //TODO: Magic string
        }

        var response = await responseMessage.Content.ReadFromJsonAsync<AuthResult>();
        if (responseMessage.StatusCode == HttpStatusCode.BadRequest || !responseMessage.IsSuccessStatusCode)
        {
            return new ErrorResult(ConcatErrors(response.Errors.ToList())); //TODO: Magic string
        }

        _httpContextAccessor.HttpContext?.Session.SetString("Token", response.Token);
        _httpContextAccessor.HttpContext?.Response.Cookies.Append("RefreshToken", response.RefreshToken);

        return new SuccessResult("Giriş Başarılı"); //TODO: Magic string
    }

    public async Task<IResult> RegisterAsync(RegisterVM registerVM)
    {
        var responseMessage = await _httpClient.PostAsJsonAsync("/api/v1/Accounts/Register", registerVM);
        if (responseMessage is null || !(responseMessage.Content.Headers.ContentLength > 0))
        {
            return new ErrorResult($"Kayıt İşlemi Başarısız - {responseMessage?.ReasonPhrase}"); //TODO: Magic string
        }

        var response = await responseMessage.Content.ReadFromJsonAsync<AuthResult>();
        if (responseMessage.StatusCode == HttpStatusCode.BadRequest || !responseMessage.IsSuccessStatusCode)
        {
            return new ErrorResult(ConcatErrors(response.Errors.ToList())); //TODO: Magic string
        }

        return new SuccessResult("Kayıt İşlemi Başarılı"); //TODO: Magic string
    }

    private string ConcatErrors(List<string> errors)
    {
        StringBuilder stringBuilder = new();

        foreach (var error in errors)
        {
            stringBuilder.Append("**");
            stringBuilder.Append(error);
        }

        return stringBuilder.ToString();
    }
}