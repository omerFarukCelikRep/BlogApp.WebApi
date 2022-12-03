using BlogApp.Business.Constants;
using BlogApp.Business.Interfaces;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.Dtos.PublishedArticles;
using BlogApp.Entities.Dtos.Users;

namespace BlogApp.Business.Concrete;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IDataResult<List<UserListDto>>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync(false);

        if (!users.Any())
        {
            return new ErrorDataResult<List<UserListDto>>(ServiceMessages.UserNotFound);
        }

        return new SuccessDataResult<List<UserListDto>>(ObjectMapper.Mapper.Map<List<UserListDto>>(users), ServiceMessages.UsersListed);
    }

    public async Task<IDataResult<UserDto>> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id, false);

        if (user is null)
        {
            return new ErrorDataResult<UserDto>(ServiceMessages.UserNotFound);
        }

        return new SuccessDataResult<UserDto>(ObjectMapper.Mapper.Map<UserDto>(user), ServiceMessages.UserGetted);
    }

    public async Task<IDataResult<PublishedArticleUserInfoDto>> GetArticleUserInfoById(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId, false);
        if (user is null)
        {
            return new ErrorDataResult<PublishedArticleUserInfoDto>(ServiceMessages.UserNotFound);
        }

        return new SuccessDataResult<PublishedArticleUserInfoDto>(ObjectMapper.Mapper.Map<PublishedArticleUserInfoDto>(user), ServiceMessages.UserGetted);
    }

    public async Task<IDataResult<UserUpdatedDto>> UpdateAsync(UserUpdateDto userUpdateDto)
    {
        var user = await _userRepository.GetByIdAsync(userUpdateDto.Id);
        if (user is null)
        {
            return new ErrorDataResult<UserUpdatedDto>("User Not Found"); //TODO: Magic string
        }

        var mappedUser = ObjectMapper.Mapper.Map(userUpdateDto, user);
        var updatedUser = await _userRepository.UpdateAsync(mappedUser);
        await _userRepository.SaveChangesAsync();

        return new SuccessDataResult<UserUpdatedDto>(ObjectMapper.Mapper.Map<UserUpdatedDto>(updatedUser), ServiceMessages.UserUpdateSuccess);
    }
}
