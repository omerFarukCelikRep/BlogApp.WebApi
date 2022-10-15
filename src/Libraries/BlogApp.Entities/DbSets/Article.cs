using BlogApp.Core.Entities.Base;

namespace BlogApp.Entities.DbSets;

public class Article : AuditableEntity
{
    public Article()
    {
        ArticleTopics = new HashSet<ArticleTopic>();
        Comments = new HashSet<Comment>();
    }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int ReadTime { get; set; }
    public string? Thumbnail { get; set; }

    //Navigation Prop.
    public Guid MemberId { get; set; }
    public virtual Member Member { get; set; }

    public virtual ICollection<ArticleTopic> ArticleTopics { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
}