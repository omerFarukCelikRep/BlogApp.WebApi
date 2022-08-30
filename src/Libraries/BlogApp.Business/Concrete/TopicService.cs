using BlogApp.Business.Interfaces;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Results.Abstract;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.DataAccess.Interfaces.Repositories;
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
    public async Task<IDataResult<TopicDto>> AddAsync(CreateTopicDto createDto)
    {
        if (await _topicRepository.AnyAsync(x => string.Equals(x.Name, createDto.Name)))
        {
            return new ErrorDataResult<TopicDto>("Duplicate Name"); //TODO: Magic string
        }

        var topic = ObjectMapper.Mapper.Map<Topic>(createDto);

        var addedTopic = await _topicRepository.AddAsync(topic);

        _ = await _topicRepository.SaveChangesAsync();

        var topicDto = ObjectMapper.Mapper.Map<TopicDto>(addedTopic);

        return new SuccessDataResult<TopicDto>(topicDto, "Successfully Added");  //TODO:Magic string
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var topic = await _topicRepository.GetByIdAsync(id);

        await _topicRepository.DeleteAsync(topic);

        _ = await _topicRepository.SaveChangesAsync();

        return new SuccessResult("Successfully Deleted"); // TODO: Magic string
    }

    public async Task<IDataResult<IEnumerable<ListTopicDto>>> GetAllAsync()
    {
        var dbTopicList = await _topicRepository.GetAllAsync(false);

        var topics = ObjectMapper.Mapper.Map<IEnumerable<ListTopicDto>>(dbTopicList);

        return new SuccessDataResult<IEnumerable<ListTopicDto>>(topics, "Topics Listed"); //TODO: Magic string
    }

    public async Task<IDataResult<IEnumerable<ListTopicDto>>> GetAllAsync(Expression<Func<Topic, bool>> expression)
    {
        var dbTopicList = await _topicRepository.GetAllAsync(expression, false);

        //if (dbTopicList is not List<Topic> { Count: <= 0 }) null ve eleman sayısı kontrolü
        if (dbTopicList == null || dbTopicList.Count() <= 0)
        {
            return new ErrorDataResult<IEnumerable<ListTopicDto>>("Topics couldn't find"); //TODO: Magic string
        }

        var topics = ObjectMapper.Mapper.Map<IEnumerable<ListTopicDto>>(dbTopicList);

        return new SuccessDataResult<IEnumerable<ListTopicDto>>(topics, "Topics Listed"); //TODO: Magic string
    }

    public async Task<IDataResult<TopicDto>> GetAsync(Expression<Func<Topic, bool>> expression)
    {
        var dbTopic = await _topicRepository.GetAsync(expression, false);

        if (dbTopic == null)
        {
            return new ErrorDataResult<TopicDto>("Topic couldn't find"); //TODO: Magic string
        }

        var topic = ObjectMapper.Mapper.Map<TopicDto>(dbTopic);

        return new SuccessDataResult<TopicDto>(topic, "Successfully getted"); //TODO: Magic string
    }

    public async Task<IDataResult<TopicDto>> GetByIdAsync(Guid id)
    {
        var dbTopic = await _topicRepository.GetByIdAsync(id, false);

        if (dbTopic == null)
        {
            return new ErrorDataResult<TopicDto>("Topic couldn't find"); //TODO: Magic string
        }

        var topic = ObjectMapper.Mapper.Map<TopicDto>(dbTopic);

        return new SuccessDataResult<TopicDto>(topic, "Topic Successfully getted"); //TODO: Magic string
    }

    public async Task<IDataResult<TopicDto>> UpdateAsync(UpdateTopicDto updateDto)
    {
        var dbTopic = await _topicRepository.GetByIdAsync(updateDto.Id);

        if (dbTopic == null)
        {
            return new ErrorDataResult<TopicDto>("Topic couldn't find"); //TODO: Magic string
        }

        var updatedTopic = ObjectMapper.Mapper.Map(updateDto, dbTopic);

        updatedTopic = await _topicRepository.UpdateAsync(updatedTopic);

        _ = await _topicRepository.SaveChangesAsync();

        var topic = ObjectMapper.Mapper.Map<TopicDto>(updatedTopic);

        return new SuccessDataResult<TopicDto>(topic, "Topic Successfully updated"); //TODO: Magic string
    }
}
