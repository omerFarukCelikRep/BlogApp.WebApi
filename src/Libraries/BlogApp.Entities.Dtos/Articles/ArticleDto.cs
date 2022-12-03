namespace BlogApp.Entities.Dtos.Articles;
public class ArticleDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int ReadTime { get; set; }
    public string? Thumbnail { get; set; }
    public Guid UserId { get; set; }
    public string AuthorName { get; set; } = null!;
    public bool IsPublished { get; set; }
    public DateTime? PublishedDate { get; set; }
}
