using AutoMapper;
using BlogApp.Entities.Concrete;
using BlogApp.Entities.Dtos.Articles;

namespace BlogApp.Business.Mappings.Profiles;
public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<Article, ArticleDto>();
        CreateMap<ArticleCreateDto, Article>();
    }
}
