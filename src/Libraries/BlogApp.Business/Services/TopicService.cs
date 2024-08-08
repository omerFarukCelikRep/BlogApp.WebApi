using BlogApp.Business.Interfaces;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.DbSets;
using BlogApp.Entities.Dtos.Topics;
using System.Linq.Expressions;

namespace BlogApp.Business.Concrete;
public class TopicService(ITopicRepository topicRepository)
    : ITopicService
{
    public async Task<IResult<TopicDto?>> AddAsync(TopicCreateDto createDto, CancellationToken cancellationToken = default)
    {
        if (await topicRepository.AnyAsync(x => string.Equals(x.Name, createDto.Name, StringComparison.OrdinalIgnoreCase), cancellationToken))
            return Result<TopicDto?>.Failure(new("400", "Duplicate Name")); //TODO: Magic string

        var topic = ObjectMapper.Mapper.Map<Topic>(createDto);
        var addedTopic = await topicRepository.AddAsync(topic, cancellationToken);
        _ = await topicRepository.SaveChangesAsync(cancellationToken);

        var topicDto = ObjectMapper.Mapper.Map<TopicDto>(addedTopic);
        return Result<TopicDto>.Success(topicDto, "Successfully Added");  //TODO:Magic string
    }

    public async Task<IResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var topic = await topicRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (topic is null)
            return Result.Failure(new("404", "Topic Not Found")); //TODO: Magic string

        await topicRepository.DeleteAsync(topic, cancellationToken);
        await topicRepository.SaveChangesAsync(cancellationToken);

        return Result.Success("Successfully Deleted"); // TODO: Magic string
    }

    public async Task<IResult<List<TopicListDto>?>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var dbTopicList = await topicRepository.GetAllAsync(false, cancellationToken);

        var topics = ObjectMapper.Mapper.Map<List<TopicListDto>>(dbTopicList);

        return Result<List<TopicListDto>>.Success(topics, "Topics Listed"); //TODO: Magic string
    }

    public async Task<IResult<List<TopicListDto>?>> GetAllAsync(Expression<Func<Topic, bool>> expression, CancellationToken cancellationToken = default)
    {
        var dbTopicList = await topicRepository.GetAllAsync(expression, false, cancellationToken);

        if (dbTopicList == null || !dbTopicList.Any())
        {
            return Result<List<TopicListDto>>.Failure(new("404", "Topics couldn't find")); //TODO: Magic string
        }

        var topics = ObjectMapper.Mapper.Map<List<TopicListDto>>(dbTopicList);

        return Result<List<TopicListDto>>.Success(topics, "Topics Listed"); //TODO: Magic string
    }

    public async Task<IResult<TopicDto?>> GetAsync(Expression<Func<Topic, bool>> expression, CancellationToken cancellationToken = default)
    {
        var dbTopic = await topicRepository.GetAsync(expression, false, cancellationToken);

        if (dbTopic == null)
        {
            return Result<TopicDto>.Failure(new("404", "Topic couldn't find")); //TODO: Magic string
        }

        var topic = ObjectMapper.Mapper.Map<TopicDto>(dbTopic);

        return Result<TopicDto>.Success(topic, "Successfully getted"); //TODO: Magic string
    }

    public async Task<IResult<TopicDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dbTopic = await topicRepository.GetByIdAsync(id, false, cancellationToken);

        if (dbTopic == null)
        {
            return Result<TopicDto>.Failure(new("404", "Topic couldn't find")); //TODO: Magic string
        }

        var topic = ObjectMapper.Mapper.Map<TopicDto>(dbTopic);

        return Result<TopicDto>.Success(topic, "Topic Successfully getted"); //TODO: Magic string
    }

    public async Task<IResult<TopicDto?>> UpdateAsync(TopicUpdateDto updateDto, CancellationToken cancellationToken = default)
    {
        var dbTopic = await topicRepository.GetByIdAsync(updateDto.Id, cancellationToken: cancellationToken);

        if (dbTopic is null)
        {
            return Result<TopicDto>.Failure(new("404", "Topic couldn't find")); //TODO: Magic string
        }

        var updatedTopic = ObjectMapper.Mapper.Map(updateDto, dbTopic);

        updatedTopic = await topicRepository.UpdateAsync(updatedTopic, cancellationToken);

        _ = await topicRepository.SaveChangesAsync(cancellationToken);

        var topic = ObjectMapper.Mapper.Map<TopicDto>(updatedTopic);

        return Result<TopicDto>.Success(topic, "Topic Successfully updated"); //TODO: Magic string
    }
}
