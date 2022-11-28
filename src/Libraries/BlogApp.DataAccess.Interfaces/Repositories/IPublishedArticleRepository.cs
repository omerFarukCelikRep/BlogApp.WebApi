﻿using BlogApp.Core.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.DbSets;

namespace BlogApp.DataAccess.Interfaces.Repositories;
public interface IPublishedArticleRepository : IAsyncFindableRepository<PublishedArticle>, IAsyncInsertableRepository<PublishedArticle>, IAsyncOrderableRepository<PublishedArticle>, IAsyncQueryableRepository<PublishedArticle>, IAsyncRepository
{
}
