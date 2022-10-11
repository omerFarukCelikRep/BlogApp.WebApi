using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.Entities.Dtos.Members;

namespace BlogApp.Business.Interfaces;
public interface IMemberService
{
    Task<IDataResult<List<MemberDto>>> GetAllAsync();
    Task<IDataResult<MemberDto>> GetByIdAsync(Guid id);
    Task<IDataResult<MemberDto>> UpdateAsync(UpdateMemberDto updateMemberDto);
}
