using BlogApp.Authentication.Dtos.Incoming;
using FluentValidation;

namespace BlogApp.Business.Validations.UserValidators;
public class UserRegistrationValidator : AbstractValidator<UserRegistrationRequestDto>
{
    private const int EmailMaxLength = 86;
    private const int PasswordMinLength = 8;
    public UserRegistrationValidator()
    {
        RuleFor(x => x.FirstName).NotNull()
                                 .NotEmpty()
                                 .WithMessage(x => string.Join(ValidationMessages.NotEmpty, nameof(x.FirstName)));

        RuleFor(x => x.LastName).NotNull()
                                .NotEmpty()
                                .WithMessage(x => string.Join(ValidationMessages.NotEmpty, nameof(x.LastName)));

        RuleFor(x => x.Email).NotNull()
                             .NotEmpty()
                             .WithMessage(x => string.Join(ValidationMessages.NotEmpty, nameof(x.Email)))
                             .EmailAddress()
                             .WithMessage(x => string.Join(ValidationMessages.NotEmpty, nameof(x.Email)))
                             .MaximumLength(EmailMaxLength)
                             .WithMessage(x => string.Join(ValidationMessages.NotEmpty, nameof(x.Email), EmailMaxLength));

        RuleFor(x => x.Password).NotNull()
                                .NotEmpty()
                                .WithMessage(x => string.Join(ValidationMessages.NotEmpty, nameof(x.Password)))
                                .MinimumLength(PasswordMinLength)
                                .WithMessage(x => string.Join(ValidationMessages.NotEmpty, nameof(x.Password), PasswordMinLength));

        RuleFor(x => x.ConfirmedPassword).NotNull()
                                         .NotEmpty()
                                         .WithMessage(x => string.Join(ValidationMessages.NotEmpty, nameof(x.ConfirmedPassword)))
                                         .Equal(x => x.Password)
                                         .WithMessage(x => string.Join(ValidationMessages.NotMatch, nameof(x.ConfirmedPassword)));
    }
}
