using BlogApp.Core.DataAccess.Interfaces;
using BlogApp.Entities.DbSets;

namespace BlogApp.DataAccess.Interfaces.Repositories;
public interface ITopicRepository : IAsyncInsertableRepository<Topic>, IAsyncUpdateableRepository<Topic>, IAsyncDeleteableRepository<Topic>, IAsyncQueryableRepository<Topic>, IAsyncFindableRepository<Topic>, IAsyncRepository
{
}
