namespace BlogApp.MVCUI.Models.Articles;

public class ArticlePublishedDetailsVM
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Thumbnail { get; set; }
    public Guid UserId { get; set; }
    public string AuthorName { get; set; }
    public DateTime PublishDate { get; set; }
    public int CommentCount { get; set; }
    public int ReadTime { get; set; }
    public int ReadingCount { get; set; }
    public int LikeCount { get; set; }
    public List<string> Topics { get; set; }
}
