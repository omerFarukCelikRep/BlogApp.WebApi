using AutoMapper;
using BlogApp.Entities.DbSets;
using BlogApp.Entities.Dtos.PublishedArticles;

namespace BlogApp.Business.Mappings.Profiles;
public class PublishedArticleProfile : Profile
{
    public PublishedArticleProfile()
    {
        CreateMap<PublishedArticle, PublishedArticleByUserListDto>()
            .ForMember(
                dest => dest.AuthorName,
                config => config.MapFrom(src => $"{src.Article!.User!.FirstName} {src.Article.User.LastName}")
            )
           .ForMember(
                    dest => dest.Topics,
                    config => config.MapFrom(src => src.Article!.ArticleTopics.Select(x => x.Topic!.Name).ToList())
            )
           .ForMember(
                dest => dest.CommentCount,
                config => config.MapFrom(src => src.Comments.Count)
            )
           .ForMember(
                dest => dest.Title,
                config => config.MapFrom(src => src.Article!.Title)
            )
           .ForMember(
                dest => dest.Content,
                config => config.MapFrom(src => src.Article!.Content)
            )
           .ForMember(
                dest => dest.ReadTime,
                config => config.MapFrom(src => src.Article!.ReadTime)
            )
           .ForMember(
                dest => dest.Thumbnail,
                config => config.MapFrom(src => src.Article!.Thumbnail)
            );

        CreateMap<PublishedArticle, PublishedArticleListDto>()
            .ForMember(
                dest => dest.AuthorName,
                config => config.MapFrom(src => $"{src.Article!.User!.FirstName} {src.Article.User.LastName}")
            )
           .ForMember(
                    dest => dest.Topics,
                    config => config.MapFrom(src => src.Article!.ArticleTopics.Select(x => x.Topic!.Name).ToList())
            )
           .ForMember(
                dest => dest.CommentCount,
                config => config.MapFrom(src => src.Comments.Count)
            )
           .ForMember(
                dest => dest.Title,
                config => config.MapFrom(src => src.Article!.Title)
            )
           .ForMember(
                dest => dest.Content,
                config => config.MapFrom(src => src.Article!.Content)
            )
           .ForMember(
                dest => dest.ReadTime,
                config => config.MapFrom(src => src.Article!.ReadTime)
            )
           .ForMember(
                dest => dest.Thumbnail,
                config => config.MapFrom(src => src.Article!.Thumbnail)
            );

        CreateMap<PublishedArticle, PublishedArticleDetailsDto>()
            .ForMember(
                dest => dest.ReadTime,
                config => config.MapFrom(src => src.Article!.ReadTime)
            )
            .ForMember(
                dest => dest.Title,
                config => config.MapFrom(src => src.Article!.Title)
            )
            .ForMember(
                dest => dest.Content,
                config => config.MapFrom(src => src.Article!.Content)
            )
            .ForMember(
                dest => dest.Thumbnail,
                config => config.MapFrom(src => src.Article!.Thumbnail)
            )
            .ForMember(
                dest => dest.AuthorName,
                config => config.MapFrom(src => $"{src.Article!.User!.FirstName} {src.Article.User.LastName}")
            )
            .ForMember(
                dest => dest.UserId,
                config => config.MapFrom(src => src.Article!.UserId)
            )
            .ForMember(
                dest => dest.CommentCount,
                config => config.MapFrom(src => src.Comments.Count)
            )
            .ForMember(
                    dest => dest.Topics,
                    config => config.MapFrom(src => src.Article!.ArticleTopics.Select(x => x.Topic!.Name).ToList())
            );

        CreateMap<PublishedArticle, PublishedArticleShortDetailsDto>()
            .ForMember(
                dest => dest.Title,
                config => config.MapFrom(src => src.Article!.Title)
            )
            .ForMember(
                dest => dest.Thumbnail,
                config => config.MapFrom(src => src.Article!.Thumbnail)
            )
            .ForMember(
                    dest => dest.Topics,
                    config => config.MapFrom(src => src.Article!.ArticleTopics.Select(x => x.Topic!.Name).ToList())
            );
    }
}
