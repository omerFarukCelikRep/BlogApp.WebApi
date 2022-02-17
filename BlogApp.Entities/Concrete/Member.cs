using BlogApp.Core.Entities.Base;

namespace BlogApp.Entities.Concrete;

public class Member : BaseEntity
{
    public Member()
    {
        Articles = new HashSet<Article>();
        MemberFollowedTopics = new HashSet<MemberFollowedTopic>();
    }
    public string? Name { get; set; }
    public string? Biography { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public string? Url { get; set; }
    public string? IdentityId { get; set; }

    //Navigation Prop.
    public virtual ICollection<Article> Articles { get; set; }
    public virtual ICollection<MemberFollowedTopic> MemberFollowedTopics { get; set; }
}