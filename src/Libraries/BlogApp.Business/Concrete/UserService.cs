using BlogApp.Business.Constants;
using BlogApp.Business.Interfaces;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.Dtos.Users;

namespace BlogApp.Business.Concrete;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IDataResult<List<UserDto>>> GetAllAsync()
    {
        var members = await _userRepository.GetAllAsync(false);

        if (!members.Any())
        {
            return new ErrorDataResult<List<UserDto>>(ServiceMessages.UserNotFound);
        }

        return new SuccessDataResult<List<UserDto>>(ObjectMapper.Mapper.Map<List<UserDto>>(members), ServiceMessages.UsersListed);
    }

    public async Task<IDataResult<UserDto>> GetByIdAsync(Guid id)
    {
        var member = await _userRepository.GetByIdAsync(id, false);

        if (member is null)
        {
            return new ErrorDataResult<UserDto>(ServiceMessages.UserNotFound);
        }

        return new SuccessDataResult<UserDto>(ObjectMapper.Mapper.Map<UserDto>(member), ServiceMessages.UserGetted);
    }

    public async Task<IDataResult<UserDto>> UpdateAsync(UserUpdateDto updateMemberDto)
    {
        var member = await _userRepository.GetByIdAsync(updateMemberDto.Id);

        var mappedMember = ObjectMapper.Mapper.Map(updateMemberDto, member);

        var updatedMember = await _userRepository.UpdateAsync(mappedMember);

        _ = await _userRepository.SaveChangesAsync();

        return new SuccessDataResult<UserDto>(ObjectMapper.Mapper.Map<UserDto>(updatedMember), ServiceMessages.UserUpdateSuccess);
    }
}
