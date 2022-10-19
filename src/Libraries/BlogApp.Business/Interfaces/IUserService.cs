using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.Entities.Dtos.Users;

namespace BlogApp.Business.Interfaces;
public interface IUserService
{
    Task<IDataResult<List<UserDto>>> GetAllAsync();
    Task<IDataResult<UserDto>> GetByIdAsync(Guid id);
    Task<IDataResult<UserDto>> UpdateAsync(UserUpdateDto updateMemberDto);
}
