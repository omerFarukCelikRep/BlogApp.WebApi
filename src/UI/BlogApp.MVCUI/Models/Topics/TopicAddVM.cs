using BlogApp.MVCUI.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogApp.MVCUI.Models.Topics;

public class TopicAddVM
{
    [Required]
    [MinLength(0)]
    [Display(Name = "İsim")]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Resim")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public IFormFile ThumbnailFile { get; set; }

    public string Thumbnail
    {
        get
        {
            if (ThumbnailFile is null)
            {
                return string.Empty;
            }
            return ThumbnailFile.FileToString().GetAwaiter().GetResult();
        }
    }
}
