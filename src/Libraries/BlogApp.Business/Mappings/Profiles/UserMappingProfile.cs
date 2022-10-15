using AutoMapper;
using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Entities.DbSets;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Business.Mappings.Profiles;
public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserRegistrationRequestDto, IdentityUser<Guid>>()
            .AfterMap((s, d) => d.EmailConfirmed = true)
            .AfterMap((s, d) => d.UserName = s.Email);

        CreateMap<UserRegistrationRequestDto, Member>();
    }
}
