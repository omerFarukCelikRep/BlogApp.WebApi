namespace BlogApp.Entities.Dtos.Articles;
public class ArticleUnpublishedDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int ReadTime { get; set; }
    public string? Thumbnail { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<string> Topics { get; set; }
    public string AuthorName { get; set; } = string.Empty;
}
