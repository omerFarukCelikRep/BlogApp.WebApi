using BlogApp.Core.DataAccess.Interfaces;
using BlogApp.Entities.Concrete;

namespace BlogApp.DataAccess.Interfaces.Repositories;
public interface ITopicRepository : IInsertableRepositoryAsync<Topic>, IUpdateableRepositoryAsync<Topic>, IDeleteableRepositoryAsync<Topic>, IQueryableRepositoryAsync<Topic>, IFindableRepositoryAsync<Topic>, IRepositoryAsync
{
}
