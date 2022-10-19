namespace BlogApp.Entities.Dtos.Topics;
public class TopicDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Thumbnail { get; set; } = string.Empty;
}
