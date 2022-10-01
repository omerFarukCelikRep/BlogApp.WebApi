namespace BlogApp.Entities.Dtos.Articles;
public class ArticleListDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int ReadTime { get; set; }
    public string? Thumbnail { get; set; }
    public Guid AuthorId { get; set; }
    public string AuthorName { get; set; }
}
