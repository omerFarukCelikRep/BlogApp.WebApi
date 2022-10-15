namespace BlogApp.Entities.Dtos.Articles;
public class ArticlePublishedListDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int ReadTime { get; set; }
    public string? Thumbnail { get; set; }
    public string AuthorName { get; set; }
    public DateTime PublishDate { get; set; }
    public int ReadingCount { get; set; }
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
    public List<string> Topics { get; set; }
}
