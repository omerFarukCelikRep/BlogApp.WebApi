using BlogApp.Core.Entities.Mapping;
using BlogApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Mapping;
public class MemberFollowedTopicMap : BaseEntityMap<MemberFollowedTopic>
{
    public override void Configure(EntityTypeBuilder<MemberFollowedTopic> builder)
    {
        builder.HasOne(x => x.Member).WithMany(x => x.MemberFollowedTopics).HasForeignKey(x => x.MemberId);
        builder.HasOne(x => x.Topic).WithMany(x => x.MemberFollowedTopics).HasForeignKey(x => x.TopicId);

        base.Configure(builder);
    }
}
