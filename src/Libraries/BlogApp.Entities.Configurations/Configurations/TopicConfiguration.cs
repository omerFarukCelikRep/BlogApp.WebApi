using BlogApp.Entities.Configurations.SeedData;

namespace BlogApp.Entities.Configurations.Configurations;
public class TopicConfiguration : BaseEntityConfiguration<Topic>
{
    private const string TableName = "Topics";
    public override void Configure(EntityTypeBuilder<Topic> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableName);

        builder.Property(x => x.Name)
               .HasMaxLength(256)
               .IsRequired();
        builder.Property(x => x.Description)
               .HasMaxLength(512)
               .IsRequired();
        builder.Property(x => x.Thumbnail)
               .IsRequired();

        builder.HasData(DataGenerator.Topics);
    }
}