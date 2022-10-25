namespace BlogApp.Entities.Dtos.Comments;
public class ArticleCommentListDto
{
    public string UserName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string Text { get; set; } = string.Empty;
}
