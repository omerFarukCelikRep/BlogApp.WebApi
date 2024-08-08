using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.Entities.Dtos.PublishedArticles;
using BlogApp.Entities.Dtos.Users;

namespace BlogApp.Business.Interfaces;
public interface IUserService
{
    Task<IResult<List<UserListDto>?>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IResult<PublishedArticleUserInfoDto?>> GetArticleUserInfoById(Guid userId, CancellationToken cancellationToken = default);
    Task<IResult<UserDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IResult<UserUpdatedDto?>> UpdateAsync(UserUpdateDto updateMemberDto, CancellationToken cancellationToken = default);
}