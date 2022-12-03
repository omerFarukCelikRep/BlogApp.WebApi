namespace BlogApp.Entities.Dtos.Comments;
public class CommentCreatedDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Text { get; set; } = null!;
    public Guid ArticleId { get; set; }
}
