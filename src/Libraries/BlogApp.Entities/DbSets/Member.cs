using BlogApp.Core.Entities.Base;

namespace BlogApp.Entities.DbSets;

public class Member : AuditableEntity
{
    public Member()
    {
        Articles = new HashSet<Article>();
        MemberFollowedTopics = new HashSet<MemberFollowedTopic>();
    }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Biography { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public string? Url { get; set; }

    public Guid? IdentityId { get; set; }

    //Navigation Prop.
    public virtual ICollection<Article> Articles { get; set; }
    public virtual ICollection<MemberFollowedTopic> MemberFollowedTopics { get; set; }
}