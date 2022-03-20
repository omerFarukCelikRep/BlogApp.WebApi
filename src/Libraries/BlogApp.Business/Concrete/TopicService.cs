using AutoMapper;
using BlogApp.Business.Abstract;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.DataAccess.Abstract;
using BlogApp.Entities.Concrete;
using BlogApp.Entities.Dtos.Topics;
using System.Linq.Expressions;

namespace BlogApp.Business.Concrete;
public class TopicService : ITopicService
{
    private readonly ITopicRepository _topicRepository;

    public TopicService(ITopicRepository topicRepository)
    {
        _topicRepository = topicRepository;
    }
    public async Task<DataResult<TopicDto>> AddAsync(CreateTopicDto createDto)
    {
        var topic = ObjectMapper.Mapper.Map<Topic>(createDto);

        topic.CreatedBy = Guid.NewGuid(); //TODO: Düzeltilecek

        var addedTopic = await _topicRepository.AddAsync(topic);

        var topicDto = ObjectMapper.Mapper.Map<TopicDto>(addedTopic);

        return new SuccessDataResult<TopicDto>(topicDto, "Successfully Added");  //TODO:Magic string
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var topic = await _topicRepository.GetByIdAsync(id);

        await _topicRepository.DeleteAsync(topic);

        return new SuccessResult("Successfully Deleted"); // TODO: Magic string
    }

    public async Task<DataResult<IEnumerable<ListTopicDto>>> GetAllAsync()
    {
        var dbTopicList = await _topicRepository.GetAllAsync();

        var topics = ObjectMapper.Mapper.Map<IEnumerable<ListTopicDto>>(dbTopicList);

        return new SuccessDataResult<IEnumerable<ListTopicDto>>(topics, "Topics Listed"); //TODO: Magic string
    }

    public async Task<DataResult<IEnumerable<ListTopicDto>>> GetAllAsync(Expression<Func<Topic, bool>> expression)
    {
        var dbTopicList = await _topicRepository.GetAllAsync(expression);

        //if (dbTopicList is not List<Topic> { Count: <= 0 }) null ve eleman sayısı kontrolü
        if (dbTopicList == null || dbTopicList.Count() <= 0)
        {
            return new ErrorDataResult<IEnumerable<ListTopicDto>>("Topics couldn't find"); //TODO: Magic string
        }

        var topics = ObjectMapper.Mapper.Map<IEnumerable<ListTopicDto>>(dbTopicList);

        return new SuccessDataResult<IEnumerable<ListTopicDto>>(topics, "Topics Listed"); //TODO: Magic string
    }

    public async Task<DataResult<TopicDto>> GetAsync(Expression<Func<Topic, bool>> expression)
    {
        var dbTopic = await _topicRepository.GetAsync(expression);

        if (dbTopic == null)
        {
            return new ErrorDataResult<TopicDto>("Topic couldn't find"); //TODO: Magic string
        }

        var topic = ObjectMapper.Mapper.Map<TopicDto>(dbTopic);

        return new SuccessDataResult<TopicDto>(topic, "Successfully getted"); //TODO: Magic string
    }

    public async Task<DataResult<TopicDto>> GetByIdAsync(Guid id)
    {
        var dbTopic = await _topicRepository.GetByIdAsync(id);

        if (dbTopic == null)
        {
            return new ErrorDataResult<TopicDto>("Topic couldn't find"); //TODO: Magic string
        }

        var topic = ObjectMapper.Mapper.Map<TopicDto>(dbTopic);

        return new SuccessDataResult<TopicDto>(topic, "Topic Successfully getted"); //TODO: Magic string
    }

    public async Task<DataResult<TopicDto>> UpdateAsync(UpdateTopicDto updateDto)
    {
        var dbTopic = await _topicRepository.GetByIdAsync(updateDto.Id);

        if (dbTopic == null)
        {
            return new ErrorDataResult<TopicDto>("Topic couldn't find"); //TODO: Magic string
        }

        var updatedTopic = ObjectMapper.Mapper.Map<Topic>(updateDto);

        await _topicRepository.UpdateAsync(updatedTopic);

        var topic = ObjectMapper.Mapper.Map<TopicDto>(updatedTopic);

        return new SuccessDataResult<TopicDto>(topic, "Topic Successfully updated"); //TODO: Magic string
    }
}
