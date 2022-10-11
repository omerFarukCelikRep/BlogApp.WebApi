using BlogApp.Core.Entities.Mapping;
using BlogApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Mapping;
public class ArticleMap : AuditableEntityMap<Article>
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
