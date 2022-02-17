using BlogApp.Core.DataAccess.Base.EntityFramework.Mapping;
using BlogApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Mapping;
public class ArticleTopicMap : BaseMap<ArticleTopic>
{
    public override void Configure(EntityTypeBuilder<ArticleTopic> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.Article).WithMany(x => x.ArticleTopics).HasForeignKey(x => x.ArticleId);
        builder.HasOne(x => x.Topic).WithMany(x => x.ArticleTopics).HasForeignKey(x => x.TopicId);
    }
}
