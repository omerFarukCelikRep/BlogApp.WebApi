using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.Entities.DbSets;
using BlogApp.Entities.Dtos.Topics;
using System.Linq.Expressions;

namespace BlogApp.Business.Interfaces;
public interface ITopicService
{
    Task<IResult<List<TopicListDto>?>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IResult<List<TopicListDto>?>> GetAllAsync(Expression<Func<Topic, bool>> expression, CancellationToken cancellationToken = default);
    Task<IResult<TopicDto?>> GetAsync(Expression<Func<Topic, bool>> expression, CancellationToken cancellationToken = default);
    Task<IResult<TopicDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IResult<TopicDto?>> AddAsync(TopicCreateDto createDto, CancellationToken cancellationToken = default);
    Task<IResult<TopicDto?>> UpdateAsync(TopicUpdateDto updateDto, CancellationToken cancellationToken = default);
    Task<IResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}