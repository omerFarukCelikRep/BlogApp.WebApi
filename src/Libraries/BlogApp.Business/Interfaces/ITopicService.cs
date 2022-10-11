using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.Entities.Concrete;
using BlogApp.Entities.Dtos.Topics;
using System.Linq.Expressions;

namespace BlogApp.Business.Interfaces;
public interface ITopicService
{
    Task<IDataResult<IEnumerable<ListTopicDto>>> GetAllAsync();
    Task<IDataResult<IEnumerable<ListTopicDto>>> GetAllAsync(Expression<Func<Topic, bool>> expression);
    Task<IDataResult<TopicDto>> GetAsync(Expression<Func<Topic, bool>> expression);
    Task<IDataResult<TopicDto>> GetByIdAsync(Guid id);
    Task<IDataResult<TopicDto>> AddAsync(CreateTopicDto createDto);
    Task<IDataResult<TopicDto>> UpdateAsync(UpdateTopicDto updateDto);
    Task<IResult> DeleteAsync(Guid id);
}
