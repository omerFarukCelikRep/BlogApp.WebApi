using BlogApp.Core.Entities.Configurations;
using BlogApp.Entities.DbSets;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Entities.Configurations;
public class CommentConfiguration : AuditableEntityConfiguration<Comment>
{
    public override void Configure(EntityTypeBuilder<Comment> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.UserName).IsRequired();
        builder.Property(x => x.UserIpAdress).IsRequired();
        builder.Property(x => x.ArticleId).IsRequired();
        builder.Property(x => x.MemberId).IsRequired(false);

        builder.HasOne(x => x.Article).WithMany(x => x.Comments).HasForeignKey(x => x.ArticleId);
    }
}
