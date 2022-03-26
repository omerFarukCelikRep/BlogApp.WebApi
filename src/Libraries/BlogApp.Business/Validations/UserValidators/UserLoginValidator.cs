using BlogApp.Authentication.Dtos.Incoming;
using FluentValidation;

namespace BlogApp.Business.Validations.UserValidators;
public class UserLoginValidator : AbstractValidator<UserLoginRequestDto>
{
    public UserLoginValidator()
    {
        RuleFor(x => x.Email).NotNull()
                             .WithMessage("Email Adresi Boş Geçilemez") //TODO: Magic string
                             .NotEmpty()
                             .WithMessage("Email Adresi Boş Geçilemez") //TODO: Magic string
                             .EmailAddress()
                             .WithMessage("Lütfen Geçerli Bir Email Adresi Giriniz") //TODO: Magic string 
                             .MaximumLength(86)
                             .WithMessage("Email Adresi 86 karakterden fazla olamaz"); //TODO: Magic string

        RuleFor(x => x.Password).NotNull()
                                .WithMessage("Şifre Boş Geçilemez")
                                .NotEmpty()
                                .WithMessage("Şifre Boş Geçilemez")
                                .MinimumLength(6) // TODO : Magic number
                                .WithMessage("Şifre 6 karakterden uzun olmalıdır"); //TODO: Magic string
    }
}
