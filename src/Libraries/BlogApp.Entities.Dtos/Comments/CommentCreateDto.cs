namespace BlogApp.Entities.Dtos.Comments;
public class CommentCreateDto
{
    public string UserName { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public Guid? UserId { get; set; }
    public Guid ArticleId { get; set; }
    public string UserIpAdress { get; set; } = string.Empty;
}
