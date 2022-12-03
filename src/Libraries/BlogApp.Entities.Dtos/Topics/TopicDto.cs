namespace BlogApp.Entities.Dtos.Topics;
public class TopicDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Thumbnail { get; set; } = null!;
}
