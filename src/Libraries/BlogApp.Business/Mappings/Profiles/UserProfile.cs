using AutoMapper;
using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Entities.DbSets;
using BlogApp.Entities.Dtos.Users;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Business.Mappings.Profiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRegistrationRequestDto, IdentityUser<Guid>>()
            .AfterMap((s, d) => d.EmailConfirmed = true)
            .AfterMap((s, d) => d.UserName = s.Email);

        CreateMap<UserRegistrationRequestDto, User>();

        CreateMap<User, UserDto>();
    }
}
