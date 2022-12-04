using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.MVCUI.Models.Articles;
using BlogApp.MVCUI.Services.Interfaces;

namespace BlogApp.MVCUI.Services.Concretes;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IDataResult<ArticleAuthorInfoVM>?> GetArticleUserInfo(Guid userId)
    {
        return await _httpClient.GetFromJsonAsync<DataResult<ArticleAuthorInfoVM>>($"/api/v1/Users/GetUserInfo?userId={userId}");
    }
}
