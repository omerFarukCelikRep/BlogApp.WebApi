using BlogApp.MVCUI.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogApp.MVCUI.Models.Topics;

public class TopicAddVM
{
    [Required]
    [MinLength(0)]
    [Display(Name = "İsim")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Resim")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public IFormFile ThumbnailFile { get; set; } = null!;

    public string? Thumbnail => ThumbnailFile?.FileToString().GetAwaiter().GetResult();
}
