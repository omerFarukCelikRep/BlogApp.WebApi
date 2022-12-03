namespace BlogApp.Entities.Dtos.Comments;
public class ArticleCommentListDto
{
    public string UserName { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public string Text { get; set; } = null!;
}
