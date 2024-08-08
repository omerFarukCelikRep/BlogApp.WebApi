using BlogApp.Business.Constants;
using BlogApp.Business.Interfaces;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.Dtos.PublishedArticles;
using BlogApp.Entities.Dtos.Users;
using UserMessages = BlogApp.Business.Constants.ServiceMessages.User;

namespace BlogApp.Business.Concrete;
public class UserService(IUserRepository userRepository)
    : IUserService
{
    public async Task<IResult<List<UserListDto>?>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var users = await userRepository.GetAllAsync(false, cancellationToken);

        if (!users.Any())
        {
            return Result<List<UserListDto>>.Failure(new("404", UserMessages.NotFound));
        }

        return Result<List<UserListDto>>.Success(ObjectMapper.Mapper.Map<List<UserListDto>>(users), UserMessages.Listed);
    }

    public async Task<IResult<UserDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(id, false, cancellationToken);

        if (user is null)
        {
            return Result<UserDto>.Failure(new("404",UserMessages.NotFound));
        }

        return Result<UserDto>.Success(ObjectMapper.Mapper.Map<UserDto>(user), UserMessages.Getted);
    }

    public async Task<IResult<PublishedArticleUserInfoDto?>> GetArticleUserInfoById(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(userId, false, cancellationToken);
        if (user is null)
        {
            return Result<PublishedArticleUserInfoDto>.Failure(new("404",UserMessages.NotFound));
        }

        return Result<PublishedArticleUserInfoDto>.Success(ObjectMapper.Mapper.Map<PublishedArticleUserInfoDto>(user), UserMessages.Getted);
    }

    public async Task<IResult<UserUpdatedDto?>> UpdateAsync(UserUpdateDto userUpdateDto, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(userUpdateDto.Id, cancellationToken: cancellationToken);
        if (user is null)
        {
            return Result<UserUpdatedDto>.Failure(new("404",UserMessages.NotFound));
        }

        var mappedUser = ObjectMapper.Mapper.Map(userUpdateDto, user);
        var updatedUser = await userRepository.UpdateAsync(mappedUser, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);

        return Result<UserUpdatedDto>.Success(ObjectMapper.Mapper.Map<UserUpdatedDto>(updatedUser), UserMessages.UpdateSuccess);
    }
}