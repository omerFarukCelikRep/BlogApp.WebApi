using BlogApp.Core.Entities.Base;

namespace BlogApp.Entities.DbSets;
public class Comment : AuditableEntity
{
    public string UserName { get; set; }
    public string UserIpAdress { get; set; }

    //Navigation Prop.
    public Guid? MemberId { get; set; }
    public Guid ArticleId { get; set; }
    public virtual PublishedArticle Article { get; set; }
}
