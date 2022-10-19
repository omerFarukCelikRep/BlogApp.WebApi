using BlogApp.Core.Entities.Base;

namespace BlogApp.Entities.DbSets;

public class Topic : BaseEntity
{
    public Topic()
    {
        ArticleTopics = new HashSet<ArticleTopic>();
        MemberFollowedTopics = new HashSet<UserFollowedTopic>();
    }
    public string Name { get; set; } = null!;
    public string Thumbnail { get; set; } = null!;

    //Navigation Prop.
    public virtual ICollection<ArticleTopic> ArticleTopics { get; set; }
    public virtual ICollection<UserFollowedTopic> MemberFollowedTopics { get; set; }
}