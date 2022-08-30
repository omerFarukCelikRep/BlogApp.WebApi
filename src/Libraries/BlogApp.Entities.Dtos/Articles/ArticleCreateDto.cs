namespace BlogApp.Entities.Dtos.Articles;
public class ArticleCreateDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public byte[]? Thumbnail { get; set; }
    public Guid MemberId { get; set; }
    public List<Guid> Topics { get; set; }
}
