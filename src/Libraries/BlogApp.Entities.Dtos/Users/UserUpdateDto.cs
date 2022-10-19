namespace BlogApp.Entities.Dtos.Users;
public class UserUpdateDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Username { get; set; }
    public string? Biography { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Url { get; set; }
}
