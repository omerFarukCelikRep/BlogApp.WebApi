using BlogApp.MVCUI.Models.Authentication;
using IResult = BlogApp.Core.Utilities.Results.Abstract.IResult;

namespace BlogApp.MVCUI.Services.Interfaces;

public interface IIdentityService
{
    bool IsLoggedIn { get; }

    string GetUserToken();
    Task<IResult> LoginAsync(LoginVM loginVM);
    Task<IResult> RegisterAsync(RegisterVM registerVM);
}
