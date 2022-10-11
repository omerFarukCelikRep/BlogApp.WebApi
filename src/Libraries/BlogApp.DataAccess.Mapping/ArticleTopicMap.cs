﻿using BlogApp.Core.Entities.Mapping;
using BlogApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Mapping;
public class ArticleTopicMap : BaseEntityMap<ArticleTopic>
{
    public override void Configure(EntityTypeBuilder<ArticleTopic> builder)
    {
        builder.HasOne(x => x.Article).WithMany(x => x.ArticleTopics).HasForeignKey(x => x.ArticleId);
        builder.HasOne(x => x.Topic).WithMany(x => x.ArticleTopics).HasForeignKey(x => x.TopicId);

        base.Configure(builder);
    }
}
