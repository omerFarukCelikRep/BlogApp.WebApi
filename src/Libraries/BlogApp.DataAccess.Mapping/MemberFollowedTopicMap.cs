using BlogApp.Core.DataAccess.Base.EntityFramework.Mapping;
using BlogApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Mapping;
public class MemberFollowedTopicMap : BaseMap<MemberFollowedTopic>
{
    public override void Configure(EntityTypeBuilder<MemberFollowedTopic> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.Member).WithMany(x => x.MemberFollowedTopics).HasForeignKey(x => x.MemberId);
        builder.HasOne(x => x.Topic).WithMany(x => x.MemberFollowedTopics).HasForeignKey(x => x.TopicId);
    }
}
