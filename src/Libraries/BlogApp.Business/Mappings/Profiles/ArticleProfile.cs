using AutoMapper;
using BlogApp.Entities.Concrete;
using BlogApp.Entities.Dtos.Articles;

namespace BlogApp.Business.Mappings.Profiles;
public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<Article, ArticleListDto>()
            .ForMember(dest => dest.AuthorName, x => x.MapFrom(src => $"{src.Member.FirstName} {src.Member.LastName}"));
        CreateMap<PublishedArticle, ArticlePublishedListDto>();
        CreateMap<Article, ArticleUnpublishedListDto>()
                .ForMember(
                    dest => dest.CreatedDate,
                    config => config.MapFrom(src => DateOnly.FromDateTime(src.CreatedDate))
                )
                .ForMember(
                    dest => dest.AuthorName,
                    config => config.MapFrom(src => $"{src.Member.FirstName} {src.Member.LastName}")
                )
                .ForMember(
                    dest => dest.Topics,
                    config => config.MapFrom(src => string.Join(", ", src.ArticleTopics.Select(x => x.Topic.Name)))
                );
        CreateMap<Article, ArticleDto>();
        CreateMap<ArticleCreateDto, Article>().ForSourceMember(member => member.Topics, opt => opt.DoNotValidate());
    }
}
