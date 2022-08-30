using BlogApp.Entities.Dtos.Articles;
using FluentValidation;

namespace BlogApp.Business.Validations.ArticleValidators;

public class ArticleCreateValidator : AbstractValidator<ArticleCreateDto>
{
    public ArticleCreateValidator()
    {
        RuleFor(x => x.Content)
            .NotNull().WithMessage("Boş Geçilemez") //TODO:Magic String
            .NotEmpty().WithMessage("Boş Geçilemez"); //TODO:Magic String

        RuleFor(x => x.Title)
            .NotNull().WithMessage("Boş Geçilemez") //TODO:Magic String
            .NotEmpty().WithMessage("Boş Geçilemez") //TODO:Magic String
            .MinimumLength(3).WithMessage("{title} Minimum {minimumLength} Olmalı"); //TODO:Magic String

        RuleFor(x => x.Topics)
            .NotNull().WithMessage("Boş Geçilemez") //TODO:Magic String
            .NotEmpty().WithMessage("Boş Geçilemez") //TODO:Magic String
            .Must(x => x.Count > 0).WithMessage("En az Bir Konu Seçilmeli"); //TODO:Magic String
    }
}
