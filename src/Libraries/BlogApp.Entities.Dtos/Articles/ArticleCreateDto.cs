using System.Text.Json.Serialization;

namespace BlogApp.Entities.Dtos.Articles;
public class ArticleCreateDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Thumbnail { get; set; }

    [JsonIgnore]
    public Guid MemberId { get; set; }

    [JsonPropertyName("topicIds")]
    public List<Guid> Topics { get; set; }
}
