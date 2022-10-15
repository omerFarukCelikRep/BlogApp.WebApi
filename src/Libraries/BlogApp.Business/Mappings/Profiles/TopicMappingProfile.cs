using AutoMapper;
using BlogApp.Entities.DbSets;
using BlogApp.Entities.Dtos.Topics;

namespace BlogApp.Business.Mappings.Profiles;

public class TopicMappingProfile : Profile
{
    public TopicMappingProfile()
    {
        CreateMap<CreateTopicDto, Topic>();

        CreateMap<UpdateTopicDto, Topic>();

        CreateMap<ListTopicDto, Topic>().ReverseMap();

        CreateMap<DetailsTopicDto, Topic>().ReverseMap();

        CreateMap<TopicDto, Topic>().ReverseMap();
    }
}
