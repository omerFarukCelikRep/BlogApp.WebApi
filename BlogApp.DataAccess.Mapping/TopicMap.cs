using BlogApp.Core.DataAccess.Base.EntityFramework.Mapping;
using BlogApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Mapping;
public class TopicMap : BaseMap<Topic>
{
    public override void Configure(EntityTypeBuilder<Topic> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Thumbnail).IsRequired();
    }
}
