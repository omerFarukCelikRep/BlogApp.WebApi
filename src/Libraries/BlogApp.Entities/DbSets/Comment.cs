namespace BlogApp.Entities.DbSets;
public class Comment : AuditableEntity
{
    public string UserName { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string UserIpAdress { get; set; } = null!;

    //Navigation Prop.
    public Guid? UserId { get; set; }
    public Guid ArticleId { get; set; }
    public virtual PublishedArticle? Article { get; set; }
}
