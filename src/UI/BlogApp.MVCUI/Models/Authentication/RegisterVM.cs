using System.ComponentModel.DataAnnotations;

namespace BlogApp.MVCUI.Models.Authentication;

public class RegisterVM
{
    [Required]
    [Display(Name = "İsim")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Soyisim")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Şifre")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Compare(nameof(Password))]
    [Display(Name = "Şifre Tekrar")]
    [DataType(DataType.Password)]
    public string ConfirmedPassword { get; set; } = string.Empty;
}
