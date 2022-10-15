using BlogApp.Core.Entities.Base;

namespace BlogApp.Entities.DbSets;
public class PublishedArticle : AuditableEntity
{
    public DateTime PublishDate { get; set; }
    public int ReadingCount { get; set; }
    public int LikeCount { get; set; }

    //Navigation Prop.
    public virtual Article? Article { get; set; }
}