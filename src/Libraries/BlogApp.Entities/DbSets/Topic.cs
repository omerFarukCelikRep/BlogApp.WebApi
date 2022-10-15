using BlogApp.Core.Entities.Base;

namespace BlogApp.Entities.DbSets;

public class Topic : BaseEntity
{
    public Topic()
    {
        ArticleTopics = new HashSet<ArticleTopic>();
        MemberFollowedTopics = new HashSet<MemberFollowedTopic>();
    }
    public string Name { get; set; }
    public string Thumbnail { get; set; }

    //Navigation Prop.
    public virtual ICollection<ArticleTopic> ArticleTopics { get; set; }
    public virtual ICollection<MemberFollowedTopic> MemberFollowedTopics { get; set; }
}