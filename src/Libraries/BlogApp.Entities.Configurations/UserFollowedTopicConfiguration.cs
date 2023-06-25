namespace BlogApp.Entities.Configurations;
public class UserFollowedTopicConfiguration : BaseEntityConfiguration<UserFollowedTopic>
{
    private const string TableName = "UserFollowedTopics";
    public override void Configure(EntityTypeBuilder<UserFollowedTopic> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableName);

        builder.HasIndex(x => new { x.TopicId, x.UserId })
               .IsUnique();

        builder.HasOne(x => x.User)
               .WithMany(x => x.MemberFollowedTopics)
               .HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Topic)
               .WithMany(x => x.MemberFollowedTopics)
               .HasForeignKey(x => x.TopicId);
    }
}