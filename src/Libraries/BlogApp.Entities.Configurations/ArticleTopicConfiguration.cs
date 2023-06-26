using BlogApp.Entities.Configurations.SeedData;

namespace BlogApp.Entities.Configurations;
public class ArticleTopicConfiguration : BaseEntityConfiguration<ArticleTopic>
{
    private const string TableName = "ArticleTopics";

    public override void Configure(EntityTypeBuilder<ArticleTopic> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableName);

        builder.HasIndex(x => new { x.ArticleId, x.TopicId })
               .IsUnique();

        builder.HasOne(x => x.Article)
               .WithMany(x => x.ArticleTopics)
               .HasForeignKey(x => x.ArticleId);
        builder.HasOne(x => x.Topic)
               .WithMany(x => x.ArticleTopics)
               .HasForeignKey(x => x.TopicId);

        builder.HasData(DataGenerator.ArticleTopics);
    }
}