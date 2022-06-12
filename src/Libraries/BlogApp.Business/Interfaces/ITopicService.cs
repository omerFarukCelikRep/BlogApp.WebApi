using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Entities.Concrete;
using BlogApp.Entities.Dtos.Topics;
using System.Linq.Expressions;

namespace BlogApp.Business.Interfaces;
public interface ITopicService
{
    Task<DataResult<IEnumerable<ListTopicDto>>> GetAllAsync();
    Task<DataResult<IEnumerable<ListTopicDto>>> GetAllAsync(Expression<Func<Topic, bool>> expression);
    Task<DataResult<TopicDto>> GetAsync(Expression<Func<Topic, bool>> expression);
    Task<DataResult<TopicDto>> GetByIdAsync(Guid id);
    Task<DataResult<TopicDto>> AddAsync(CreateTopicDto createDto);
    Task<DataResult<TopicDto>> UpdateAsync(UpdateTopicDto updateDto);
    Task<Result> DeleteAsync(Guid id);
}
