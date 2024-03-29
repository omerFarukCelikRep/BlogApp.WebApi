﻿using BlogApp.Entities.Dtos.Topics;
using FluentValidation;

namespace BlogApp.Business.Validations.TopicValidators;
public class TopicUpdateValidator : AbstractValidator<TopicUpdateDto>
{
    public TopicUpdateValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("Konu ismi boş geçilemez") //TODO: Magic string
            .NotEmpty()
            .WithMessage("Konu ismi boş geçilemez") //TODO: Magic string
            .MinimumLength(3)
            .WithMessage("Konu ismi minimum 3 karakter olmalıdır."); // TODO: Magic string
    }
}
