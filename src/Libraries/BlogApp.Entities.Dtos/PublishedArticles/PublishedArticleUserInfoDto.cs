namespace BlogApp.Entities.Dtos.PublishedArticles;
public class PublishedArticleUserInfoDto
{
    public string AuthorName { get; set; } = null!;
    public string? Image { get; set; }
    public string? Biography { get; set; }
}
