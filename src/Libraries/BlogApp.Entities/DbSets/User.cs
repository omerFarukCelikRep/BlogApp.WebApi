namespace BlogApp.Entities.DbSets;

public class User : AuditableEntity
{
    public User()
    {
        Articles = new HashSet<Article>();
        MemberFollowedTopics = new HashSet<UserFollowedTopic>();
    }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? Biography { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Url { get; set; }

    //Navigation Prop.
    public virtual ICollection<Article> Articles { get; set; }
    public virtual ICollection<UserFollowedTopic> MemberFollowedTopics { get; set; }
}