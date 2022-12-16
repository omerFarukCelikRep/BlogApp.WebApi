using BlogApp.Entities.Dtos.Topics;

namespace BlogApp.Entities.Dtos.Articles;
public class ArticleUnpublishedDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int ReadTime { get; set; }
    public string? Thumbnail { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<TopicArticleDetailsDto> Topics { get; set; } = new();
    public string AuthorName { get; set; } = null!;
}
