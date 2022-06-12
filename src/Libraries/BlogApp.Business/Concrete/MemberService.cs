using BlogApp.Business.Constants;
using BlogApp.Business.Interfaces;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Results.Abstract;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.Dtos.Members;

namespace BlogApp.Business.Concrete;
public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<IDataResult<List<MemberDto>>> GetAllAsync()
    {
        var members = await _memberRepository.GetAllAsync(false);

        if (!members.Any())
        {
            return new ErrorDataResult<List<MemberDto>>(ServiceMessages.MemberNotFound);
        }

        return new SuccessDataResult<List<MemberDto>>(ObjectMapper.Mapper.Map<List<MemberDto>>(members), ServiceMessages.MembersListed);
    }

    public async Task<IDataResult<MemberDto>> GetByIdAsync(Guid id, bool tracking = true)
    {
        var member = await _memberRepository.GetByIdAsync(id, tracking);

        if (member is null)
        {
            return new ErrorDataResult<MemberDto>(ServiceMessages.MemberNotFound);
        }

        return new SuccessDataResult<MemberDto>(ObjectMapper.Mapper.Map<MemberDto>(member), ServiceMessages.MemberGetted);
    }

    public async Task<IDataResult<MemberDto>> UpdateAsync(UpdateMemberDto updateMemberDto)
    {
        var member = await _memberRepository.GetByIdAsync(updateMemberDto.Id);

        var mappedMember = ObjectMapper.Mapper.Map(updateMemberDto, member);

        var updatedMember = await _memberRepository.UpdateAsync(mappedMember);

        return new SuccessDataResult<MemberDto>(ObjectMapper.Mapper.Map<MemberDto>(updatedMember), ServiceMessages.MemberUpdateSuccess);
    }
}
