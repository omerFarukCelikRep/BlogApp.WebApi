namespace BlogApp.Entities.Dtos.Topics;
public class TopicListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Thumbnail { get; set; } = null!;
}
