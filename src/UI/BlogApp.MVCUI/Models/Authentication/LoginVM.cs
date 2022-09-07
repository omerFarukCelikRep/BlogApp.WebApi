using System.ComponentModel.DataAnnotations;

namespace BlogApp.MVCUI.Models.Authentication;

public class LoginVM
{
    [Required]
    [Display(Name = "Email")]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Şifre")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
