namespace BlogApp.Entities.Dtos.Members;
public class MemberDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Biography { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public string? Url { get; set; }
}
