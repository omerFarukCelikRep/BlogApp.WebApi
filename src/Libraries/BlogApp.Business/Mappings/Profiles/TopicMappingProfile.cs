using AutoMapper;
using BlogApp.Entities.Concrete;
using BlogApp.Entities.Dtos.Topics;

namespace BlogApp.Business.Mappings.Profiles;

public class TopicMappingProfile : Profile
{
    public TopicMappingProfile()
    {
        CreateMap<CreateTopicDto, Topic>().ReverseMap();

        CreateMap<UpdateTopicDto, Topic>().ReverseMap();

        CreateMap<ListTopicDto, Topic>().ReverseMap();

        CreateMap<DetailsTopicDto, Topic>().ReverseMap();

        CreateMap<TopicDto, Topic>().ReverseMap();
    }
}
