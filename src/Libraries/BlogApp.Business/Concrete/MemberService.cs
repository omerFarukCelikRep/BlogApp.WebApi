using BlogApp.Business.Constants;
using BlogApp.Business.Interfaces;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.Dtos.Users;

namespace BlogApp.Business.Concrete;
public class MemberService : IMemberService
{
    private readonly IUserRepository _memberRepository;

    public MemberService(IUserRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<IDataResult<List<UserDto>>> GetAllAsync()
    {
        var members = await _memberRepository.GetAllAsync(false);

        if (!members.Any())
        {
            return new ErrorDataResult<List<UserDto>>(ServiceMessages.MemberNotFound);
        }

        return new SuccessDataResult<List<UserDto>>(ObjectMapper.Mapper.Map<List<UserDto>>(members), ServiceMessages.MembersListed);
    }

    public async Task<IDataResult<UserDto>> GetByIdAsync(Guid id)
    {
        var member = await _memberRepository.GetByIdAsync(id, false);

        if (member is null)
        {
            return new ErrorDataResult<UserDto>(ServiceMessages.MemberNotFound);
        }

        return new SuccessDataResult<UserDto>(ObjectMapper.Mapper.Map<UserDto>(member), ServiceMessages.MemberGetted);
    }

    public async Task<IDataResult<UserDto>> UpdateAsync(UserUpdateDto updateMemberDto)
    {
        var member = await _memberRepository.GetByIdAsync(updateMemberDto.Id);

        var mappedMember = ObjectMapper.Mapper.Map(updateMemberDto, member);

        var updatedMember = await _memberRepository.UpdateAsync(mappedMember);

        _ = await _memberRepository.SaveChangesAsync();

        return new SuccessDataResult<UserDto>(ObjectMapper.Mapper.Map<UserDto>(updatedMember), ServiceMessages.MemberUpdateSuccess);
    }
}
