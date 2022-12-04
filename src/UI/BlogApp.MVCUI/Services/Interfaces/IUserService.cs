using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.MVCUI.Models.Articles;

namespace BlogApp.MVCUI.Services.Interfaces;

public interface IUserService
{
    Task<IDataResult<ArticleAuthorInfoVM>?> GetArticleUserInfo(Guid userId);
}
