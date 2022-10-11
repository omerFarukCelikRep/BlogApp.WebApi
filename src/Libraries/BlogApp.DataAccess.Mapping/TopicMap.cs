using BlogApp.Core.Entities.Mapping;
using BlogApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Mapping;
public class TopicMap : BaseEntityMap<Topic>
{
    public override void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Thumbnail).IsRequired(false); //TODO: Zorunlu olacak

        base.Configure(builder);
    }
}
