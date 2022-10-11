using BlogApp.Core.Entities.Configurations;
using BlogApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Entities.Configurations;
public class TopicConfiguration : BaseEntityConfiguration<Topic>
{
    public override void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Thumbnail).IsRequired(false); //TODO: Zorunlu olacak

        base.Configure(builder);
    }
}
