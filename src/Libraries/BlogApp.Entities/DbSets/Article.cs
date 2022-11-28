using BlogApp.Core.Entities.Base;

namespace BlogApp.Entities.DbSets;

public class Article : AuditableEntity
{
    public Article()
    {
        ArticleTopics = new HashSet<ArticleTopic>();
    }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int? ReadTime { get; set; }
    public string? Thumbnail { get; set; }

    //Navigation Prop.
    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<ArticleTopic> ArticleTopics { get; set; }
}