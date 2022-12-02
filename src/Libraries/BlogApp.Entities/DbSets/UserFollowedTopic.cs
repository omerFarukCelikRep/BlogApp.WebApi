namespace BlogApp.Entities.DbSets;

public class UserFollowedTopic : BaseEntity
{
    //Navigation Prop.
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }
    public Guid TopicId { get; set; }
    public virtual Topic? Topic { get; set; }
}