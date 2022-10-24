using AutoMapper;
using BlogApp.Entities.DbSets;
using BlogApp.Entities.Dtos.Comments;

namespace BlogApp.Business.Mappings.Profiles;
public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CommentCreateDto, Comment>();
        CreateMap<Comment, CommentCreatedDto>();
    }
}
