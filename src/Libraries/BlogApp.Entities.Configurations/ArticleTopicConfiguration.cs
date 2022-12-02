namespace BlogApp.Entities.Configurations;
public class ArticleTopicConfiguration : BaseEntityConfiguration<ArticleTopic>
{
    public override void Configure(EntityTypeBuilder<ArticleTopic> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.Article).WithMany(x => x.ArticleTopics).HasForeignKey(x => x.ArticleId);
        builder.HasOne(x => x.Topic).WithMany(x => x.ArticleTopics).HasForeignKey(x => x.TopicId);
    }
}
