namespace BlogApp.Entities.Dtos.Users;
public class UserUpdatedDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Biography { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Url { get; set; }
}
