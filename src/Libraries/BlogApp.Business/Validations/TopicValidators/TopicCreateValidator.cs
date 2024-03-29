﻿using BlogApp.Entities.Dtos.Topics;
using FluentValidation;

namespace BlogApp.Business.Validations.TopicValidators;
public class TopicCreateValidator : AbstractValidator<TopicCreateDto>
{
    public TopicCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("Konu ismi boş geçilemez") //TODO: Magic string
            .NotEmpty()
            .WithMessage("Konu ismi boş geçilemez") //TODO: Magic string
            .MinimumLength(3);
    }
}
