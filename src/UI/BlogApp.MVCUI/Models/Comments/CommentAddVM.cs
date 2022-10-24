using System.ComponentModel.DataAnnotations;

namespace BlogApp.MVCUI.Models.Comments;

public class CommentAddVM
{
    [Required]
    public Guid ArticleId { get; set; }

    [Required]
    [Display(Name = "Name")]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Comment")]
    public string Text { get; set; } = string.Empty;

}
