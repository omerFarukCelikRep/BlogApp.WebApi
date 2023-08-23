namespace BlogApp.Entities.DbSets;
public class UserSession : BaseEntity
{
    public string Token { get; set; } = null!;
    public DateTime ExpireDate { get; set; }
    public string IpAddress { get; set; } = null!;

    //Navigation Prop.
    public Guid UserId { get; set; }
}
