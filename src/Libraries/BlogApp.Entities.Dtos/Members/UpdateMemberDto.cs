namespace BlogApp.Entities.Dtos.Members;
public class UpdateMemberDto
{
    public string? Name { get; set; }
    public string? Username { get; set; }
    public string? Biography { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public string? Url { get; set; }
    public string? PhoneNumber { get; set; }
}
