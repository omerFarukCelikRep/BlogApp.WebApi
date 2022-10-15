using AutoMapper;
using BlogApp.Entities.DbSets;
using BlogApp.Entities.Dtos.Articles;

namespace BlogApp.Business.Mappings.Profiles;
public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<Article, ArticleListDto>()
            .ForMember(dest => dest.AuthorName, x => x.MapFrom(src => $"{src.Member.FirstName} {src.Member.LastName}"));

        CreateMap<PublishedArticle, ArticlePublishedListDto>()
            .ForMember(
                dest => dest.AuthorName,
                config => config.MapFrom(src => $"{src.Article.Member.FirstName} {src.Article.Member.LastName}")
            )
           .ForMember(
                    dest => dest.Topics,
                    config => config.MapFrom(src => src.Article.ArticleTopics.Select(x => x.Topic.Name).ToList())
            )
           .ForMember(
                dest => dest.CommentCount,
                config => config.MapFrom(src => src.Comments.Count)
            )
           .ForMember(
                dest => dest.Title,
                config => config.MapFrom(src => src.Article.Title)
            )
           .ForMember(
                dest => dest.Content,
                config => config.MapFrom(src => src.Article.Content)
            )
           .ForMember(
                dest => dest.ReadTime,
                config => config.MapFrom(src => src.Article.ReadTime)
            )
           .ForMember(
                dest => dest.Thumbnail,
                config => config.MapFrom(src => src.Article.Thumbnail)
            );

        CreateMap<Article, ArticleUnpublishedListDto>()
                .ForMember(
                    dest => dest.AuthorName,
                    config => config.MapFrom(src => $"{src.Member.FirstName} {src.Member.LastName}")
                )
                .ForMember(
                    dest => dest.Topics,
                    config => config.MapFrom(src => src.ArticleTopics.Select(x => x.Topic.Name).ToList())
                );

        CreateMap<Article, ArticleUnpublishedDetailsDto>()
            .ForMember(
                    dest => dest.AuthorName,
                    config => config.MapFrom(src => $"{src.Member.FirstName} {src.Member.LastName}")
                )
                .ForMember(
                    dest => dest.Topics,
                    config => config.MapFrom(src => src.ArticleTopics.Select(x => x.Topic.Name).ToList())
            );

        CreateMap<Article, ArticleDto>();

        CreateMap<ArticleCreateDto, Article>().ForSourceMember(member => member.Topics, opt => opt.DoNotValidate());
    }
}
