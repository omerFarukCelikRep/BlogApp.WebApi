using AutoMapper;
using BlogApp.Authentication.Dtos.Incoming;
using BlogApp.Entities.DbSets;
using BlogApp.Entities.Dtos.PublishedArticles;
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

        CreateMap<User, UserListDto>();

        CreateMap<User, UserDto>();

        CreateMap<User, PublishedArticleUserInfoDto>()
            .ForMember(
                dest => dest.AuthorName,
                config => config.MapFrom(src => $"{src.FirstName} {src.LastName}")
            )
            .ForMember(
                dest => dest.Image,
                config => config.MapFrom(src => src.ProfilePicture)
            );

        CreateMap<UserUpdateDto, User>();

        CreateMap<User, UserUpdatedDto>();
    }
}
