using AutoMapper;
using BlogApp.Entities.DbSets;
using BlogApp.Entities.Dtos.Members;

namespace BlogApp.Business.Mappings.Profiles;
public class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<Member, MemberDto>();
    }
}
