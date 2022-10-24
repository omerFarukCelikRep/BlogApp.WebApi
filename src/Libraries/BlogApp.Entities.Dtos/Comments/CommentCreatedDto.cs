namespace BlogApp.Entities.Dtos.Comments;
public class CommentCreatedDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Text { get; set; }
    public Guid ArticleId { get; set; }
}
