using BlogApp.Core.Entities.Base;

namespace BlogApp.Entities.Concrete;

public class ArticleTopic : BaseEntity
{
    //Navigation Prop.
    public Guid ArticleId { get; set; }
    public virtual Article? Article { get; set; }
    public Guid TopicId { get; set; }
    public virtual Topic? Topic { get; set; }
}