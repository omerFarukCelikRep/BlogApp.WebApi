using BlogApp.Core.Entities.Mapping;
using BlogApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Mapping;
public class PublishedArticleMap : AuditableEntityMap<PublishedArticle>
{
    public override void Configure(EntityTypeBuilder<PublishedArticle> builder)
    {
        builder.Property(x => x.PublishDate).IsRequired();
        builder.Property(x => x.ReadingCount).HasDefaultValue(0);
        builder.Property(x => x.LikeCount).HasDefaultValue(0);

        builder.HasOne(x => x.Article).WithMany().HasForeignKey(x => x.Id);

        base.Configure(builder);
    }
}
