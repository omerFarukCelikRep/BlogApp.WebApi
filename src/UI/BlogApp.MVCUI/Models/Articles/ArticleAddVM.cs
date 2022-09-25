using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogApp.MVCUI.Models.Articles;

public class ArticleAddVM
{
    [Required]
    [MinLength(0)]
    [Display(Name = "Başlık")]
    public string Title { get; set; }

    [Required]
    [Display(Name = "İçerik")]
    public string Content { get; set; }
    public string Thumbnail { get; set; }

    [Required]
    [Display(Name = "Resim")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public IFormFile ThumbnailFile { get; set; }

    [Required]
    [Display(Name = "Konular")]
    public List<Guid> TopicIds { get; set; }

    public List<SelectListItem> Topics { get; set; }
}
