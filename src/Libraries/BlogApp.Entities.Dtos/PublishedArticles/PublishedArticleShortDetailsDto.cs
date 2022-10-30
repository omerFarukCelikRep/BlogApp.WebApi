namespace BlogApp.Entities.Dtos.PublishedArticles;
public class PublishedArticleShortDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Thumbnail { get; set; }
    public List<string> Topics { get; set; }
}
