namespace BlogApp.MVCUI.Models.Articles;

public class ArticleUnpublishedListVM
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int ReadTime { get; set; }
    public string? Thumbnail { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Topics { get; set; }
    public string AuthorName { get; set; }
}
