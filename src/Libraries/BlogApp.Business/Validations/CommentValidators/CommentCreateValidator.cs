using BlogApp.Entities.Dtos.Comments;
using FluentValidation;

namespace BlogApp.Business.Validations.CommentValidators;
public class CommentCreateValidator : AbstractValidator<CommentCreateDto>
{
    public CommentCreateValidator()
    {
        RuleFor(x => x.UserName)
            .NotNull().WithMessage("Boş Geçilemez") //TODO:Magic String
            .NotEmpty().WithMessage("Boş Geçilemez") //TODO:Magic String
            .MaximumLength(512);

        RuleFor(x => x.Text)
        .NotNull().WithMessage("Boş Geçilemez") //TODO:Magic String
        .NotEmpty().WithMessage("Boş Geçilemez"); //TODO:Magic String

        RuleFor(x => x.ArticleId)
            .NotNull().WithMessage("Boş Geçilemez"); //TODO:Magic String
    }
}
