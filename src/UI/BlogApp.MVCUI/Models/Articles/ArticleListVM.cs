namespace BlogApp.MVCUI.Models.Articles;

public class ArticleListVM
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string? Thumbnail { get; set; }

}
