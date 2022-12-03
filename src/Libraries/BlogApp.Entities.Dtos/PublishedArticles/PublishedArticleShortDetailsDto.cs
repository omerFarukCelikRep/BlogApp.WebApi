namespace BlogApp.Entities.Dtos.PublishedArticles;
public class PublishedArticleShortDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Thumbnail { get; set; } = null!;
    public List<string> Topics { get; set; } = new();
}
