using BlogApp.Authentication.Dtos.Incoming;
using FluentValidation;

namespace BlogApp.Business.Validations.UserValidators;
public class UserRegistrationValidator : AbstractValidator<UserRegistrationRequestDto>
{
    public UserRegistrationValidator()
    {
        RuleFor(x => x.FirstName).NotNull()
                                 .NotEmpty()
                                 .WithMessage("İsim boş bırakılamaz"); //TODO: Magic string

        RuleFor(x => x.LastName).NotNull()
                                .NotEmpty()
                                .WithMessage("Soyisim boş bırakılamaz"); //TODO: Magic string

        RuleFor(x => x.Email).NotNull()
                             .NotEmpty()
                             .WithMessage("Email Adresi Boş Geçilemez") //TODO: Magic string
                             .EmailAddress()
                             .WithMessage("Lütfen Geçerli Bir Email Adresi Giriniz") //TODO: Magic string 
                             .MaximumLength(86)
                             .WithMessage("Email Adresi 86 karakterden fazla olamaz"); //TODO: Magic string

        RuleFor(x => x.Password).NotNull()
                                .NotEmpty()
                                .WithMessage("Şifre boş bırakılamaz") //TODO: Magic string
                                .MinimumLength(8)
                                .WithMessage("Şifre minimum 8 karakter olmalıdır"); //TODO: Magic string

        RuleFor(x => x.ConfirmedPassword).NotNull()
                                         .NotEmpty()
                                         .WithMessage("Şifre tekrarı boş bırakılamaz") //TODO: Magic string
                                         .Equal(x => x.Password)
                                         .WithMessage("Şifre eşleşmemektedir"); //TODO : Magic string
    }
}
