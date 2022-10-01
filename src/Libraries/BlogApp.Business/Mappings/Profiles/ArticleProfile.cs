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
        CreateMap<Article, ArticleUnpublishedListDto>();
        CreateMap<Article, ArticleDto>();
        CreateMap<ArticleCreateDto, Article>().ForSourceMember(member => member.Topics, opt => opt.DoNotValidate());
    }
}
