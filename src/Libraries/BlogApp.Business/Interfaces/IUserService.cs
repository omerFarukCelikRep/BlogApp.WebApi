using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.Entities.Dtos.PublishedArticles;
using BlogApp.Entities.Dtos.Users;

namespace BlogApp.Business.Interfaces;
public interface IUserService
{
    Task<IDataResult<List<UserListDto>>> GetAllAsync();
    Task<IDataResult<PublishedArticleUserInfoDto>> GetArticleUserInfoById(Guid userId);
    Task<IDataResult<UserDto>> GetByIdAsync(Guid id);
    Task<IDataResult<UserUpdatedDto>> UpdateAsync(UserUpdateDto updateMemberDto);
}
