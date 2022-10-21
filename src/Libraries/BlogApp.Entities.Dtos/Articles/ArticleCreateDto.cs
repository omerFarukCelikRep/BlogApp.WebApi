using System.Text.Json.Serialization;

namespace BlogApp.Entities.Dtos.Articles;
public class ArticleCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? Thumbnail { get; set; }

    [JsonIgnore]
    public Guid UserId { get; set; }

    [JsonPropertyName("topicIds")]
    public List<Guid> Topics { get; set; }
}
