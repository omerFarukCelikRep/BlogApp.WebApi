using BlogApp.Entities.DbSets;
using System;
using System.Collections.Generic;

namespace BlogApp.UnitTests.Fixtures;
public static class ArticlesFixture
{
    public static List<Article> GetTestArticles() => new()
    {
        new()
        {
            Id = Guid.NewGuid(),
            Title = "Test Article 1",
            Content = "Test Article 1 Content",
            ReadTime = 10,
            UserId = Guid.NewGuid()
        },
        new()
        {
            Id = Guid.NewGuid(),
            Title = "Test Article 2",
            Content = "Test Article 2 Content",
            ReadTime = 10,
            UserId = Guid.NewGuid()
        },
        new()
        {
            Id = Guid.NewGuid(),
            Title = "Test Article 3",
            Content = "Test Article 3 Content",
            ReadTime = 10,
            UserId = Guid.NewGuid()
        },
        new()
        {
            Id = Guid.NewGuid(),
            Title = "Test Article 4",
            Content = "Test Article 4 Content",
            ReadTime = 10,
            UserId = Guid.NewGuid()
        },
        new()
        {
            Id = Guid.NewGuid(),
            Title = "Test Article 5",
            Content = "Test Article 5 Content",
            ReadTime = 10,
            UserId = Guid.NewGuid()
        }
    };
}
