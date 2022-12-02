namespace BlogApp.Entities.Configurations;
public class UserFollowedTopicConfiguration : BaseEntityConfiguration<UserFollowedTopic>
{
    public override void Configure(EntityTypeBuilder<UserFollowedTopic> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.User).WithMany(x => x.MemberFollowedTopics).HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Topic).WithMany(x => x.MemberFollowedTopics).HasForeignKey(x => x.TopicId);
    }
}
