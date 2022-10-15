using BlogApp.Core.Entities.Configurations;
using BlogApp.Entities.DbSets;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Entities.Configurations;
public class MemberFollowedTopicConfiguration : BaseEntityConfiguration<MemberFollowedTopic>
{
    public override void Configure(EntityTypeBuilder<MemberFollowedTopic> builder)
    {
        builder.HasOne(x => x.Member).WithMany(x => x.MemberFollowedTopics).HasForeignKey(x => x.MemberId);
        builder.HasOne(x => x.Topic).WithMany(x => x.MemberFollowedTopics).HasForeignKey(x => x.TopicId);

        base.Configure(builder);
    }
}
