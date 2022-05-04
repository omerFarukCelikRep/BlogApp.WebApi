using BlogApp.Core.DataAccess.Base.EntityFramework.Mapping;
using BlogApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Mapping;
public class ArticleMap : BaseMap<Article>
{
    public override void Configure(EntityTypeBuilder<Article> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.Title).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Thumbnail).IsRequired();
        builder.Property(x => x.ReadTime).IsRequired();

        builder.HasOne(x => x.Member).WithMany(x => x.Articles).HasForeignKey(x => x.MemberId);
    }
}
