namespace BlogApp.Entities.Dtos.Members;
public class MemberDto
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Biography { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public string? Url { get; set; }
}
