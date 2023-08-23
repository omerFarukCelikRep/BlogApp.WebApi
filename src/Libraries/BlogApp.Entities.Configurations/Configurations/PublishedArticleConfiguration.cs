namespace BlogApp.Entities.Configurations.Configurations;

public class PublishedArticleConfiguration : AuditableEntityConfiguration<PublishedArticle>
{
    private const string TableName = "PublishedArticles";
    public override void Configure(EntityTypeBuilder<PublishedArticle> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableName);

        builder.Property(x => x.PublishDate)
               .IsRequired();
        builder.Property(x => x.ReadingCount)
               .HasDefaultValue(default);
        builder.Property(x => x.LikeCount)
               .HasDefaultValue(default);

        builder.HasOne(x => x.Article)
               .WithMany()
               .HasForeignKey(x => x.Id);
    }
}