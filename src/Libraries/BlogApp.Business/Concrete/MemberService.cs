using AutoMapper.Internal.Mappers;
using BlogApp.Business.Abstract;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Results.Abstract;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.DataAccess.Abstract;
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

        return new SuccessDataResult<List<MemberDto>>(ObjectMapper.Mapper.Map<List<MemberDto>>(members));
    }
}
