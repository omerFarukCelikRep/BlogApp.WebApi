using BlogApp.Core.Utilities.Results.Abstract;
using BlogApp.Entities.Dtos.Members;

namespace BlogApp.Business.Interfaces;
public interface IMemberService
{
    Task<IDataResult<List<MemberDto>>> GetAllAsync();
    Task<IDataResult<MemberDto>> GetByIdAsync(Guid id, bool tracking = true);
    Task<IDataResult<MemberDto>> UpdateAsync(UpdateMemberDto updateMemberDto);
}
