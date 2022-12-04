using BlogApp.MVCUI.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogApp.MVCUI.Models.Articles;

public class ArticleAddVM
{
    [Required]
    [MinLength(0)]
    [Display(Name = "Başlık")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Display(Name = "İçerik")]
    public string Content { get; set; } = string.Empty;
    public string? Thumbnail => ThumbnailFile?.FileToString().GetAwaiter().GetResult();

    [Display(Name = "Resim")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public IFormFile? ThumbnailFile { get; set; }

    [Required]
    [Display(Name = "Konular")]
    public List<Guid> TopicIds { get; set; } = new();

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public IEnumerable<SelectListItem>? Topics { get; set; }
}