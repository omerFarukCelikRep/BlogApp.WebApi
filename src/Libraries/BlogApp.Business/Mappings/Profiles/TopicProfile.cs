using AutoMapper;
using BlogApp.Entities.DbSets;
using BlogApp.Entities.Dtos.Topics;

namespace BlogApp.Business.Mappings.Profiles;

public class TopicProfile : Profile
{
    public TopicProfile()
    {
        CreateMap<TopicCreateDto, Topic>();

        CreateMap<TopicUpdateDto, Topic>();

        CreateMap<Topic, TopicListDto>();

        CreateMap<Topic,TopicDetailsDto>().ReverseMap();

        CreateMap<TopicDto, Topic>().ReverseMap();
    }
}
