using AutoMapper;
using BlogApp.Business.Mappings.Profiles;

namespace BlogApp.Business.Mappings.Mapper;
public static class ObjectMapper
{
    private static readonly Lazy<IMapper> _mapper = new Lazy<IMapper>(() =>
    {
        var configuration = new MapperConfiguration(config =>
        {
            config.AddProfile<TopicProfile>();
            config.AddProfile<UserProfile>();
            config.AddProfile<ArticleProfile>();
            config.AddProfile<PublishedArticleProfile>();
            config.AddProfile<CommentProfile>();
        });

        return configuration.CreateMapper();
    });

    public static IMapper Mapper => _mapper.Value;
}
