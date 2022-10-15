using BlogApp.Core.Entities.Configurations;
using BlogApp.Entities.DbSets;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Entities.Configurations;

public class ArticleConfiguration : AuditableEntityConfiguration<Article>
{
    public override void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.Title).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Thumbnail).IsRequired();
        builder.Property(x => x.ReadTime).IsRequired();

        builder.HasOne(x => x.Member).WithMany(x => x.Articles).HasForeignKey(x => x.MemberId);

        base.Configure(builder);
    }
}