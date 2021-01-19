using FluentValidation;
using SocialMediaCore.DTOs;
using System;

namespace SocialMediaInfrastructure.Validators
{
    public class PostValidator: AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            RuleFor(post => post.Description)
                .NotNull()
                .Length(10, 100);

            RuleFor(post => post.Date)
                .NotNull()
                .LessThan(DateTime.Now);
        }
    }
}
