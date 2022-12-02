namespace BlogApp.Entities.Configurations;
public class TopicConfiguration : BaseEntityConfiguration<Topic>
{
    public override void Configure(EntityTypeBuilder<Topic> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Thumbnail).IsRequired();
    }
}
