using AutoMapper;
using BlogApp.Entities.DbSets;
using BlogApp.Entities.Dtos.Articles;

namespace BlogApp.Business.Mappings.Profiles;
public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<Article, ArticleUnpublishedListDto>()
            .ForMember(
                dest => dest.AuthorName,
                config => config.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}")
            )
            .ForMember(
                dest => dest.Topics,
                config => config.MapFrom(src => src.ArticleTopics.Select(x => x.Topic.Name).ToList())
            );

        CreateMap<Article, ArticleUnpublishedDetailsDto>()
            .ForMember(
                    dest => dest.AuthorName,
                    config => config.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}")
                )
                .ForMember(
                    dest => dest.Topics,
                    config => config.MapFrom(src => src.ArticleTopics.Select(x => x.Topic.Name).ToList())
            );

        CreateMap<Article, ArticleDto>();

        CreateMap<ArticleCreateDto, Article>()
            .ForSourceMember(
                member => member.Topics,
                opt => opt.DoNotValidate());
    }
}
