using BlogApp.Core.Entities.Base;

namespace BlogApp.Entities.Concrete;
public class PublishedArticle : BaseEntity
{
    public DateTime PublishDate { get; set; }
    public int ReadingCount { get; set; }
    public int LikeCount { get; set; }

    //Navigation Prop.
    public virtual Article? Article { get; set; }
}