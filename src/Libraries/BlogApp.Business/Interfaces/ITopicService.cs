using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.Entities.DbSets;
using BlogApp.Entities.Dtos.Topics;
using System.Linq.Expressions;

namespace BlogApp.Business.Interfaces;
public interface ITopicService
{
    Task<IDataResult<IEnumerable<TopicListDto>>> GetAllAsync();
    Task<IDataResult<IEnumerable<TopicListDto>>> GetAllAsync(Expression<Func<Topic, bool>> expression);
    Task<IDataResult<TopicDto>> GetAsync(Expression<Func<Topic, bool>> expression);
    Task<IDataResult<TopicDto>> GetByIdAsync(Guid id);
    Task<IDataResult<TopicDto>> AddAsync(TopicCreateDto createDto);
    Task<IDataResult<TopicDto>> UpdateAsync(TopicUpdateDto updateDto);
    Task<IResult> DeleteAsync(Guid id);
}
