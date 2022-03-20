using BlogApp.Core.Entities.Base;

namespace BlogApp.Entities.Concrete;

public class MemberFollowedTopic : BaseEntity
{
    //Navigation Prop.
    public Guid MemberId { get; set; }
    public virtual Member? Member { get; set; }
    public Guid TopicId { get; set; }
    public virtual Topic? Topic { get; set; }
}